namespace LaoTzu
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogTargetDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxTargetDirectory = new System.Windows.Forms.TextBox();
            this.labelNamespace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowseTargetDirectory = new System.Windows.Forms.Button();
            this.checkBoxCompile = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludePartials = new System.Windows.Forms.CheckBox();
            this.textBoxCompileFileName = new System.Windows.Forms.TextBox();
            this.textBoxPartialDirectory = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogPartialsDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonBrowsePartialsDirectory = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.comboBoxHistory = new System.Windows.Forms.ComboBox();
            this.comboBoxConnections = new System.Windows.Forms.ComboBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.listBoxReferenceAssemblies = new System.Windows.Forms.ListBox();
            this.textBoxNewReferenceAssembly = new System.Windows.Forms.TextBox();
            this.buttonAddAssemblyReference = new System.Windows.Forms.Button();
            this.buttonDeleteAssemblyReference = new System.Windows.Forms.Button();
            this.checkBoxExtractSchema = new System.Windows.Forms.CheckBox();
            this.labelSchemaFile = new System.Windows.Forms.Label();
            this.groupBoxReferenceAssemblies = new System.Windows.Forms.GroupBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.labelConfigs = new System.Windows.Forms.Label();
            this.comboBoxSchemaFiles = new System.Windows.Forms.ComboBox();
            this.groupBoxReferenceAssemblies.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNamespace.Location = new System.Drawing.Point(113, 94);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(352, 20);
            this.textBoxNamespace.TabIndex = 1;
            // 
            // textBoxTargetDirectory
            // 
            this.textBoxTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTargetDirectory.Location = new System.Drawing.Point(113, 121);
            this.textBoxTargetDirectory.Name = "textBoxTargetDirectory";
            this.textBoxTargetDirectory.ReadOnly = true;
            this.textBoxTargetDirectory.Size = new System.Drawing.Size(313, 20);
            this.textBoxTargetDirectory.TabIndex = 2;
            // 
            // labelNamespace
            // 
            this.labelNamespace.AutoSize = true;
            this.labelNamespace.Location = new System.Drawing.Point(40, 97);
            this.labelNamespace.Name = "labelNamespace";
            this.labelNamespace.Size = new System.Drawing.Size(67, 13);
            this.labelNamespace.TabIndex = 4;
            this.labelNamespace.Text = "Namespace:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Target Directory:";
            // 
            // buttonBrowseTargetDirectory
            // 
            this.buttonBrowseTargetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseTargetDirectory.Location = new System.Drawing.Point(432, 119);
            this.buttonBrowseTargetDirectory.Name = "buttonBrowseTargetDirectory";
            this.buttonBrowseTargetDirectory.Size = new System.Drawing.Size(33, 23);
            this.buttonBrowseTargetDirectory.TabIndex = 6;
            this.buttonBrowseTargetDirectory.Text = "...";
            this.buttonBrowseTargetDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseTargetDirectory.Click += new System.EventHandler(this.buttonBrowseTargetDirectory_Click);
            // 
            // checkBoxCompile
            // 
            this.checkBoxCompile.AutoSize = true;
            this.checkBoxCompile.Location = new System.Drawing.Point(44, 150);
            this.checkBoxCompile.Name = "checkBoxCompile";
            this.checkBoxCompile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxCompile.Size = new System.Drawing.Size(63, 17);
            this.checkBoxCompile.TabIndex = 7;
            this.checkBoxCompile.Text = "Compile";
            this.checkBoxCompile.UseVisualStyleBackColor = true;
            this.checkBoxCompile.CheckedChanged += new System.EventHandler(this.checkBoxCompile_CheckedChanged);
            // 
            // checkBoxIncludePartials
            // 
            this.checkBoxIncludePartials.AutoSize = true;
            this.checkBoxIncludePartials.Location = new System.Drawing.Point(9, 177);
            this.checkBoxIncludePartials.Name = "checkBoxIncludePartials";
            this.checkBoxIncludePartials.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxIncludePartials.Size = new System.Drawing.Size(98, 17);
            this.checkBoxIncludePartials.TabIndex = 8;
            this.checkBoxIncludePartials.Text = "Include Partials";
            this.checkBoxIncludePartials.UseVisualStyleBackColor = true;
            this.checkBoxIncludePartials.CheckedChanged += new System.EventHandler(this.checkBoxIncludePartials_CheckedChanged);
            // 
            // textBoxCompileFileName
            // 
            this.textBoxCompileFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCompileFileName.Location = new System.Drawing.Point(113, 148);
            this.textBoxCompileFileName.Name = "textBoxCompileFileName";
            this.textBoxCompileFileName.ReadOnly = true;
            this.textBoxCompileFileName.Size = new System.Drawing.Size(352, 20);
            this.textBoxCompileFileName.TabIndex = 9;
            // 
            // textBoxPartialDirectory
            // 
            this.textBoxPartialDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPartialDirectory.Location = new System.Drawing.Point(113, 175);
            this.textBoxPartialDirectory.Name = "textBoxPartialDirectory";
            this.textBoxPartialDirectory.ReadOnly = true;
            this.textBoxPartialDirectory.Size = new System.Drawing.Size(313, 20);
            this.textBoxPartialDirectory.TabIndex = 10;
            // 
            // buttonBrowsePartialsDirectory
            // 
            this.buttonBrowsePartialsDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowsePartialsDirectory.Enabled = false;
            this.buttonBrowsePartialsDirectory.Location = new System.Drawing.Point(432, 173);
            this.buttonBrowsePartialsDirectory.Name = "buttonBrowsePartialsDirectory";
            this.buttonBrowsePartialsDirectory.Size = new System.Drawing.Size(33, 23);
            this.buttonBrowsePartialsDirectory.TabIndex = 12;
            this.buttonBrowsePartialsDirectory.Text = "...";
            this.buttonBrowsePartialsDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowsePartialsDirectory.Click += new System.EventHandler(this.buttonBrowsePartialsDirectory_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(389, 442);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Location = new System.Drawing.Point(308, 442);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 14;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // comboBoxHistory
            // 
            this.comboBoxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHistory.FormattingEnabled = true;
            this.comboBoxHistory.Location = new System.Drawing.Point(113, 14);
            this.comboBoxHistory.Name = "comboBoxHistory";
            this.comboBoxHistory.Size = new System.Drawing.Size(352, 21);
            this.comboBoxHistory.TabIndex = 15;
            this.comboBoxHistory.Text = "- config history -";
            this.comboBoxHistory.SelectedIndexChanged += new System.EventHandler(this.comboBoxHistory_SelectedIndexChanged);
            // 
            // comboBoxConnections
            // 
            this.comboBoxConnections.Enabled = false;
            this.comboBoxConnections.FormattingEnabled = true;
            this.comboBoxConnections.Location = new System.Drawing.Point(113, 41);
            this.comboBoxConnections.Name = "comboBoxConnections";
            this.comboBoxConnections.Size = new System.Drawing.Size(351, 21);
            this.comboBoxConnections.TabIndex = 17;
            this.comboBoxConnections.Text = "- Select a connection from the app.config file -";
            this.comboBoxConnections.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnections_SelectedIndexChanged);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Location = new System.Drawing.Point(3, 16);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(227, 216);
            this.textBoxOutput.TabIndex = 18;
            // 
            // listBoxReferenceAssemblies
            // 
            this.listBoxReferenceAssemblies.FormattingEnabled = true;
            this.listBoxReferenceAssemblies.Location = new System.Drawing.Point(11, 19);
            this.listBoxReferenceAssemblies.Name = "listBoxReferenceAssemblies";
            this.listBoxReferenceAssemblies.Size = new System.Drawing.Size(188, 160);
            this.listBoxReferenceAssemblies.TabIndex = 19;
            // 
            // textBoxNewReferenceAssembly
            // 
            this.textBoxNewReferenceAssembly.Location = new System.Drawing.Point(11, 180);
            this.textBoxNewReferenceAssembly.Name = "textBoxNewReferenceAssembly";
            this.textBoxNewReferenceAssembly.Size = new System.Drawing.Size(188, 20);
            this.textBoxNewReferenceAssembly.TabIndex = 20;
            // 
            // buttonAddAssemblyReference
            // 
            this.buttonAddAssemblyReference.Location = new System.Drawing.Point(131, 205);
            this.buttonAddAssemblyReference.Name = "buttonAddAssemblyReference";
            this.buttonAddAssemblyReference.Size = new System.Drawing.Size(31, 23);
            this.buttonAddAssemblyReference.TabIndex = 21;
            this.buttonAddAssemblyReference.Text = "+";
            this.buttonAddAssemblyReference.UseVisualStyleBackColor = true;
            this.buttonAddAssemblyReference.Click += new System.EventHandler(this.buttonAddAssemblyReference_Click);
            // 
            // buttonDeleteAssemblyReference
            // 
            this.buttonDeleteAssemblyReference.Location = new System.Drawing.Point(168, 205);
            this.buttonDeleteAssemblyReference.Name = "buttonDeleteAssemblyReference";
            this.buttonDeleteAssemblyReference.Size = new System.Drawing.Size(31, 23);
            this.buttonDeleteAssemblyReference.TabIndex = 22;
            this.buttonDeleteAssemblyReference.Text = "-";
            this.buttonDeleteAssemblyReference.UseVisualStyleBackColor = true;
            this.buttonDeleteAssemblyReference.Click += new System.EventHandler(this.buttonDeleteAssemblyReference_Click);
            // 
            // checkBoxExtractSchema
            // 
            this.checkBoxExtractSchema.AutoSize = true;
            this.checkBoxExtractSchema.Location = new System.Drawing.Point(8, 43);
            this.checkBoxExtractSchema.Name = "checkBoxExtractSchema";
            this.checkBoxExtractSchema.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxExtractSchema.Size = new System.Drawing.Size(101, 17);
            this.checkBoxExtractSchema.TabIndex = 23;
            this.checkBoxExtractSchema.Text = "Extract Schema";
            this.checkBoxExtractSchema.UseVisualStyleBackColor = true;
            this.checkBoxExtractSchema.CheckedChanged += new System.EventHandler(this.checkBoxExtractSchema_CheckedChanged);
            // 
            // labelSchemaFile
            // 
            this.labelSchemaFile.AutoSize = true;
            this.labelSchemaFile.Location = new System.Drawing.Point(39, 71);
            this.labelSchemaFile.Name = "labelSchemaFile";
            this.labelSchemaFile.Size = new System.Drawing.Size(68, 13);
            this.labelSchemaFile.TabIndex = 25;
            this.labelSchemaFile.Text = "Schema File:";
            // 
            // groupBoxReferenceAssemblies
            // 
            this.groupBoxReferenceAssemblies.Controls.Add(this.listBoxReferenceAssemblies);
            this.groupBoxReferenceAssemblies.Controls.Add(this.textBoxNewReferenceAssembly);
            this.groupBoxReferenceAssemblies.Controls.Add(this.buttonDeleteAssemblyReference);
            this.groupBoxReferenceAssemblies.Controls.Add(this.buttonAddAssemblyReference);
            this.groupBoxReferenceAssemblies.Location = new System.Drawing.Point(13, 201);
            this.groupBoxReferenceAssemblies.Name = "groupBoxReferenceAssemblies";
            this.groupBoxReferenceAssemblies.Size = new System.Drawing.Size(212, 235);
            this.groupBoxReferenceAssemblies.TabIndex = 27;
            this.groupBoxReferenceAssemblies.TabStop = false;
            this.groupBoxReferenceAssemblies.Text = "Reference Assemblies";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.textBoxOutput);
            this.groupBoxOutput.Location = new System.Drawing.Point(231, 201);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(233, 235);
            this.groupBoxOutput.TabIndex = 28;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // labelConfigs
            // 
            this.labelConfigs.AutoSize = true;
            this.labelConfigs.Location = new System.Drawing.Point(23, 17);
            this.labelConfigs.Name = "labelConfigs";
            this.labelConfigs.Size = new System.Drawing.Size(86, 13);
            this.labelConfigs.TabIndex = 29;
            this.labelConfigs.Text = "Previous Configs";
            // 
            // comboBoxSchemaFiles
            // 
            this.comboBoxSchemaFiles.FormattingEnabled = true;
            this.comboBoxSchemaFiles.Location = new System.Drawing.Point(113, 67);
            this.comboBoxSchemaFiles.Name = "comboBoxSchemaFiles";
            this.comboBoxSchemaFiles.Size = new System.Drawing.Size(351, 21);
            this.comboBoxSchemaFiles.TabIndex = 30;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 475);
            this.Controls.Add(this.comboBoxSchemaFiles);
            this.Controls.Add(this.labelConfigs);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxReferenceAssemblies);
            this.Controls.Add(this.labelSchemaFile);
            this.Controls.Add(this.checkBoxExtractSchema);
            this.Controls.Add(this.comboBoxConnections);
            this.Controls.Add(this.comboBoxHistory);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonBrowsePartialsDirectory);
            this.Controls.Add(this.textBoxPartialDirectory);
            this.Controls.Add(this.textBoxCompileFileName);
            this.Controls.Add(this.checkBoxIncludePartials);
            this.Controls.Add(this.checkBoxCompile);
            this.Controls.Add(this.buttonBrowseTargetDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelNamespace);
            this.Controls.Add(this.textBoxTargetDirectory);
            this.Controls.Add(this.textBoxNamespace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(492, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(492, 500);
            this.Name = "Main";
            this.Text = "LaoTzu";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBoxReferenceAssemblies.ResumeLayout(false);
            this.groupBoxReferenceAssemblies.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTargetDirectory;
        private System.Windows.Forms.TextBox textBoxTargetDirectory;
        private System.Windows.Forms.Label labelNamespace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowseTargetDirectory;
        private System.Windows.Forms.CheckBox checkBoxCompile;
        private System.Windows.Forms.CheckBox checkBoxIncludePartials;
        private System.Windows.Forms.TextBox textBoxCompileFileName;
        private System.Windows.Forms.TextBox textBoxPartialDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPartialsDirectory;
        private System.Windows.Forms.Button buttonBrowsePartialsDirectory;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.ComboBox comboBoxHistory;
        private System.Windows.Forms.ComboBox comboBoxConnections;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.ListBox listBoxReferenceAssemblies;
        private System.Windows.Forms.TextBox textBoxNewReferenceAssembly;
        private System.Windows.Forms.Button buttonAddAssemblyReference;
        private System.Windows.Forms.Button buttonDeleteAssemblyReference;
        private System.Windows.Forms.CheckBox checkBoxExtractSchema;
        private System.Windows.Forms.Label labelSchemaFile;
        private System.Windows.Forms.GroupBox groupBoxReferenceAssemblies;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Label labelConfigs;
        private System.Windows.Forms.ComboBox comboBoxSchemaFiles;
    }
}

