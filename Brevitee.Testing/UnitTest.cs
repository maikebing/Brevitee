using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.CommandLine;
using System.Reflection;

namespace Brevitee.Testing
{
    /// <summary>
    /// Attribue used to mark a method as a Unit Test
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public class UnitTest: ConsoleAction, IPreAndPostInvoke
    {
        public UnitTest()
            : base()
        {
        }

        public UnitTest(string description)
            : base(description)
        {
        }
        
        public UnitTest(string preInvokeMethodName, bool isNotDescription) // this is ugly, should revisit :b
        {
            this.PreInvokeMethodName = preInvokeMethodName;            
        }

        public UnitTest(string preInvokeMethodName, string alwaysPostInvokeMethodName, string postInvokeMethodName)
        {
            this.PreInvokeMethodName = preInvokeMethodName;
            this.PostInvokeMethodName = postInvokeMethodName;
            this.AlwaysPostInvokeMethodName = alwaysPostInvokeMethodName;
        }

        public UnitTest(string description, string preInvokeMethodName = null, string alwaysPostInvokeMethodName = null, string postInvokeMethodName = null)
            : base(description)
        {
            this.PreInvokeMethodName = preInvokeMethodName;
            this.PostInvokeMethodName = postInvokeMethodName;
            this.AlwaysPostInvokeMethodName = alwaysPostInvokeMethodName;
        }
        
        #region IPreAndPostInvoke Members

        public string PreInvokeMethodName
        {
            get;
            set;
        }

        public string PostInvokeMethodName
        {
            get;
            set;
        }

        public string AlwaysPostInvokeMethodName
        {
            get;
            set;
        }

        #endregion
    }
}
