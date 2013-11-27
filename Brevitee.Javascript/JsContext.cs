using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Incubation;
using EcmaScript.NET.Types;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET;

namespace Brevitee.Javascript
{
    public class JsContext
    {
        Context _context;
        ScriptableObject _scope;
        StringBuilder _loaded;

        public JsContext()
        {
            this._context = Context.Enter();
            this._scope = _context.InitStandardObjects();
            this._loaded = new StringBuilder();
        }

        public void SetCliValue(string varName, object instance)
        {
            ScriptableObject.PutProperty(_scope, varName, instance);
        }

        /// <summary>
        /// Preload the specified script text
        /// </summary>
        /// <param name="script"></param>
        public void Load(string script)
        {
            _loaded.Append(script);
        }

        public T GetCliValue<T>(string varName)
        {
            CliObject cli = GetValue<CliObject>(varName);
            return (T)cli.Object;
        }

        public T GetValue<T>(string varName)
        {
            return (T)ScriptableObject.GetProperty(_scope, varName);            
        }
        
        public void Run(string script, string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "script_".RandomLetters(8);
            }
            StringBuilder toRun = new StringBuilder(_loaded.ToString());
            toRun.Append(script);
            _context.EvaluateString(_scope, toRun.ToString(), name, 0, null);
        }
    }
}
