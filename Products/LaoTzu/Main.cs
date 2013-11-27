using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.CodeDom.Compiler;
using System.Threading;
using Brevitee.Configuration;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Data.MsSql;
using Brevitee.Incubation;

namespace LaoTzu
{
    public partial class Main : Form
    {
        FileInfo _compileTo;
        FileInfo _schemaFile;
        History _history;
        FileInfo _historyFile;
        public Main()
        {
            InitializeComponent();
            textBoxCompileFileName.KeyUp += (s, a) =>
            {
                SetCompileTo(textBoxCompileFileName.Text);
            };

            _history = new History();
        }

        private void buttonBrowseTargetDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialogTargetDirectory.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxTargetDirectory.Text = folderBrowserDialogTargetDirectory.SelectedPath;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _historyFile = new FileInfo(".\\history.xml");
            if (_historyFile.Exists)
            {
                LoadHistory(_historyFile);
            }

            SetTargetDirectory();
            SetConnections();
            SetReferenceAssemblies();
            SetSchemaFiles();
        }

        private void SetSchemaFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(".\\Schemas");
            if (!dir.Exists)
            {
                dir.Create();
            }

            foreach (FileInfo file in dir.GetFiles("*.jsdb"))
            {
                comboBoxSchemaFiles.Items.Add(file.Name);
            }
        }

        private void SetReferenceAssemblies()
        {
            foreach (string reference in DaoGenerator.DefaultReferenceAssemblies)
            {
                listBoxReferenceAssemblies.Items.Add(reference);
            }
        }

        private void SetSchemaFile(string name)
        {
            if (!name.EndsWith(".jsdb"))
            {
                name = string.Format("{0}.jsdb", name);
            }

            _schemaFile = new FileInfo(Path.Combine(".\\Schemas\\", name));            
        }

        private void SetConfigReferenceAssemblies(GenConfig config)
        {
            List<string> defaults = DaoGenerator.DefaultReferenceAssemblies;
            foreach (object item in listBoxReferenceAssemblies.Items)
            {
                string name = item.ToString();
                if (!defaults.Contains(name))
                {
                    config.AddAssemblyReference(name);
                }
            }
        }

        private void SetConnections()
        {
            ConnectionStringSettingsCollection connections = DefaultConfiguration.GetConnectionStrings();
            foreach (ConnectionStringSettings setting in connections)
            {
                if (!setting.Name.StartsWith("Local"))
                {
                    comboBoxConnections.Items.Add(new ConnectionInfo(setting));
                }
            }
        }

        private void SetTargetDirectory()
        {
            SetTargetDirectory(".\\Generated");
        }

        private void SetTargetDirectory(string path)
        {
            DirectoryInfo targetDirectory = new DirectoryInfo(path);
            if (!targetDirectory.Exists)
            {
                targetDirectory.Create();
            }

            textBoxTargetDirectory.Text = targetDirectory.FullName;
        }

        private void SetCompileTo(string name)
        {
            _compileTo = new FileInfo(Path.Combine(".\\Compiled\\", name));
            if (!_compileTo.Directory.Exists)
            {
                _compileTo.Directory.Create();
            }
                        
            textBoxCompileFileName.Text = _compileTo.Name;
        }

        private void LoadHistory(FileInfo file)
        {
            _history = file.FullName.XmlDeserialize<History>();
            comboBoxHistory.Items.Clear();
            foreach (GenConfig config in _history.GenConfigs)
            {
                comboBoxHistory.Items.Add(config);
            }
        }
        
        private void buttonBrowsePartialsDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialogPartialsDirectory.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxPartialDirectory.Text = folderBrowserDialogPartialsDirectory.SelectedPath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            GenConfig config;
            bool valid = Validate(out config);
            if (valid)
            {
                DisableGenerate();

                textBoxOutput.Text = string.Empty;
                _history.Add(config);
                _history.ToXmlFile(_historyFile.FullName);

                if(!_schemaFile.Directory.Exists)
                {
                    _schemaFile.Directory.Create();
                }

                SchemaDefinition schema;
                if(!GetSchema(config, out schema))
                {
                    return;
                }

                Out("Beginning code generation...");
                Generate(config, schema);
                Out("Code generation complete");
                if (config.Compile)
                {
                    Out("Beginning code compilation...");
                    Compile(config);
                    Out("Code compilation complete");
                }

                LoadHistory(_historyFile);

                EnableGenerate();
            }
        }

        private void DisableGenerate()
        {
            buttonGenerate.Enabled = false;
            buttonGenerate.Text = "Generating...";
        }

        private void EnableGenerate()
        {
            buttonGenerate.Enabled = true;
            buttonGenerate.Text = "Generate";
        }

        private bool GetSchema(GenConfig config, out SchemaDefinition schema)
        {
            schema = null;
            bool gotSchema = false;
            string schemaPath = Path.Combine(".\\Schemas\\", config.SchemaFileName);
            if (config.Extract)
            {
                OutFormat("Extracting schema using ({0})...", config.Name);
                schema = ExtractSchema(config.Name, schemaPath);
                gotSchema = true;
            }
            else
            {
                FileInfo file = new FileInfo(schemaPath);
                if (!file.Exists)
                {
                    MessageBox.Show(string.Format("The schmea file {0} was not found", file.FullName));
                    gotSchema = false;
                }
                else
                {
                    schema = SchemaDefinition.Load(schemaPath);
                    gotSchema = true;
                }
            }

            return gotSchema;
        }

        private void Out()
        {
            Out("\r\n");
        }

        private void Out(string txt)
        {
            textBoxOutput.Text = string.Format("{0}\r\n{1}", textBoxOutput.Text, txt);
            textBoxOutput.SelectionStart = textBoxOutput.Text.Length;
            textBoxOutput.ScrollToCaret();
            this.Refresh();
            Thread.Sleep(30);
        }

        private void OutFormat(string format, params object[] args)
        {
            Out(string.Format(format, args));
        }

        private void Compile(GenConfig config)
        {
            DaoGenerator generator = new DaoGenerator(config.Namespace);
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            dirs.Add(new DirectoryInfo(config.TargetDirectory));
            if (config.IncludePartials)
            {
                dirs.Add(new DirectoryInfo(config.PartialDirectory));
            }
            string compileToFile = _compileTo.FullName;
            if (!compileToFile.EndsWith(".dll"))
            {
                compileToFile = string.Format("{0}.dll", compileToFile);
            }
            List<string> refAssemblies = new List<string>();
            refAssemblies.AddRange(DaoGenerator.DefaultReferenceAssemblies);
            refAssemblies.AddRange(config.ReferenceAssemblies);
            CompilerResults results = generator.Compile(dirs.ToArray(), compileToFile, refAssemblies.ToArray());
            OutputCompilerErrors(results);
        }

        private void OutputCompilerErrors(CompilerResults results)
        {
            foreach (CompilerError error in results.Errors)
            {
                OutFormat("File=>{0}", error.FileName);
                OutFormat("Line {0}, Column {1}::{2}", error.Line, error.Column, error.ErrorText);
                Out();
            }
        }

        private void Generate(GenConfig config, SchemaDefinition schema)
        {
            DaoGenerator generator = new DaoGenerator(config.Namespace);
            generator.BeforeClassParse += (ns, t) =>
            {
                Out(string.Format("Generating code for {0}.{1}", ns, t.ClassName));
            };
            
            generator.Generate(schema, config.TargetDirectory);
        }

        private static SchemaDefinition ExtractSchema(string connectionName, string filePath)
        {
            SqlClientSchemaExtractor extractor = new SqlClientSchemaExtractor(connectionName);
            SchemaDefinition schema = extractor.Extract();
            schema.Save(filePath);
            return schema;
        }

        private bool Validate(out GenConfig config)
        {
            bool valid = true;
            if (checkBoxExtractSchema.Checked && comboBoxConnections.SelectedItem == null)
            {
                valid = false;
                config = null;
                MessageBox.Show("Please select a connection");
            }
            else if (!checkBoxExtractSchema.Checked && comboBoxSchemaFiles.SelectedItem == null)
            {
                valid = false;
                config = null;
                MessageBox.Show("Please select a schema");
            }
            else if (checkBoxCompile.Checked && string.IsNullOrEmpty(textBoxCompileFileName.Text.Trim()))
            {
                valid = false;
                config = null;
                MessageBox.Show("Please specify the name to compile code to");
                textBoxCompileFileName.Focus();
            }
            else
            {
                string name = "";
                if (checkBoxExtractSchema.Checked)
                {
                    name = ((ConnectionInfo)comboBoxConnections.SelectedItem).Name;
                }
                else
                {
                    FileInfo file = new FileInfo(string.Format(".\\Schemas\\{0}", comboBoxSchemaFiles.SelectedText));
                    name = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
                }

                config = ConstructConfig(name);

                SetConfigReferenceAssemblies(config);
                SetSchemaFile(config.SchemaFileName);

                List<string> messages;
                valid = config.IsValid(out messages);
                if (!valid)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine("There was a problem with your entries\r\n\r\n");
                    foreach (string m in messages)
                    {
                        msg.AppendLine(string.Format("- {0}", m));
                    }
                    MessageBox.Show(msg.ToString());
                }
            }
            return valid;
        }

        private GenConfig ConstructConfig(string name)
        {
            GenConfig config;

            config = new GenConfig();
            config.Name = name;
            config.CompileFileName = textBoxCompileFileName.Text;
            config.Namespace = textBoxNamespace.Text;
            config.TargetDirectory = textBoxTargetDirectory.Text;
            config.PartialDirectory = textBoxPartialDirectory.Text;
            config.SchemaFileName = comboBoxSchemaFiles.Text;
            config.Extract = checkBoxExtractSchema.Checked;
            return config;
        }


        private void checkBoxCompile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompile.Checked)
            {
                textBoxCompileFileName.ReadOnly = false;
                textBoxCompileFileName.Focus();
            }
            else
            {
                textBoxCompileFileName.Text = string.Empty;
                textBoxCompileFileName.ReadOnly = true;
            }
        }

        private void checkBoxIncludePartials_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncludePartials.Checked)
            {
                textBoxPartialDirectory.ReadOnly = false;
                buttonBrowsePartialsDirectory.Enabled = true;                
            }
            else
            {
                textBoxPartialDirectory.ReadOnly = true;
                textBoxPartialDirectory.Text = string.Empty;
                buttonBrowsePartialsDirectory.Enabled = false;
            }
        }

        private void comboBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenConfig config = comboBoxHistory.SelectedItem as GenConfig;
            if (config != null)
            {
                SetConfig(config);
            }
        }

        private void SetConfig(GenConfig config)
        {
            textBoxCompileFileName.Text = config.CompileFileName;
            textBoxNamespace.Text = config.Namespace;
            textBoxTargetDirectory.Text = config.TargetDirectory;
            textBoxPartialDirectory.Text = config.PartialDirectory;
            textBoxCompileFileName.Text = config.CompileFileName;

            checkBoxExtractSchema.Checked = config.Extract;
            checkBoxCompile.Checked = config.Compile;
            checkBoxIncludePartials.Checked = config.IncludePartials;

            if (config.Compile)
            {
                SetCompileTo(config.CompileFileName);
            }

            SetSelectedConnection(config);
            SetSelectedSchemaFile(config);
            SetSchemaFile(config.SchemaFileName);

            AddReferenceAssemblies(config.ReferenceAssemblies);
        }

        private void SetSelectedSchemaFile(GenConfig config)
        {
            int l = comboBoxSchemaFiles.Items.Count;
            for (int i = 0; i < l; i++)
            {
                object item = comboBoxSchemaFiles.Items[i];
                FileInfo file = item as FileInfo;
                if (file != null && file.Name.Equals(config.SchemaFileName))
                {
                    comboBoxSchemaFiles.SelectedIndex = i;
                    break;
                }
            }
        }

        private void SetSelectedConnection(GenConfig config)
        {
            int l = comboBoxConnections.Items.Count;
            for (int i = 0; i < l; i++)
            {
                object item = comboBoxConnections.Items[i];
                ConnectionInfo info = item as ConnectionInfo;
                if (info.Name.Equals(config.Name))
                {
                    comboBoxConnections.SelectedIndex = i;
                    break;
                }
            }
        }
        
        private void AddReferenceAssemblies(params string[] names)
        {
            List<string> current = new List<string>();
            foreach (object item in listBoxReferenceAssemblies.Items)
            {
                current.Add(item.ToString());
            }
            foreach (string name in names)
            {
                if (!current.Contains(name))
                {
                    listBoxReferenceAssemblies.Items.Add(name);
                }
            }
        }

        private void buttonAddAssemblyReference_Click(object sender, EventArgs e)
        {
            string assembly = textBoxNewReferenceAssembly.Text;
            if (!string.IsNullOrEmpty(assembly))
            {
                listBoxReferenceAssemblies.Items.Add(assembly);                
            }
        }

        private void buttonDeleteAssemblyReference_Click(object sender, EventArgs e)
        {
            object item = listBoxReferenceAssemblies.SelectedItem;
            if (item != null)
            {
                listBoxReferenceAssemblies.Items.Remove(item);
            }
        }

        private void comboBoxConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionInfo conn = comboBoxConnections.SelectedItem as ConnectionInfo;
            if (conn != null)
            {
                string schemaFileName = string.Format("{0}.jsdb", conn.Name);
                EnsureSchemaInDropDown(schemaFileName);     
            }
        }

        private void EnsureSchemaInDropDown(string fileName)
        {
            bool hasIt = false;
            int l = comboBoxSchemaFiles.Items.Count;
            for (int i = 0; i < l; i++)
            {
                object item = comboBoxSchemaFiles.Items[i];
                if (item != null && item.ToString().Equals(fileName))
                {
                    hasIt = true;
                    comboBoxSchemaFiles.SelectedIndex = i;
                }
            }

            if (!hasIt)
            {
                comboBoxSchemaFiles.Items.Add(fileName);
                comboBoxSchemaFiles.SelectedIndex = comboBoxSchemaFiles.Items.Count - 1;
            }
        }

        private void checkBoxExtractSchema_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExtractSchema.Checked)
            {
                comboBoxConnections.Enabled = true;
            }
            else
            {
                comboBoxConnections.Enabled = false;
            }
        }
    }
}
