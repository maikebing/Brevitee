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
        
        public UnitTest(string before, bool isNotDescription) // this is ugly, should revisit :b
        {
            this.Before = before;            
        }

        public UnitTest(string before, string alwarsAfter, string afterSuccess)
        {
            this.Before = before;
            this.AfterSuccess = afterSuccess;
            this.AlwaysAfter = alwarsAfter;
        }

        public UnitTest(string description, string before = null, string alwaysAfter = null, string afterSuccess = null)
            : base(description)
        {
            this.Before = before;
            this.AfterSuccess = afterSuccess;
            this.AlwaysAfter = alwaysAfter;
        }
        
        #region IPreAndPostInvoke Members

        public string Before
        {
            get;
            set;
        }

        public string AfterSuccess
        {
            get;
            set;
        }

        public string AlwaysAfter
        {
            get;
            set;
        }

        #endregion
    }
}
