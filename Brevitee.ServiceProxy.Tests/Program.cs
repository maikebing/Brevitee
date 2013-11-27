using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.ServiceProxy;

namespace Brevitee.ServiceProxy.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        [ConsoleAction("Add your action here")]
        public void AddYourActionHere()
        {
        }

        class TestExecutionRequest: ExecutionRequest
        {
            public TestExecutionRequest(string c, string m, string f)
                : base(c, m, f)
            {

            }

            public bool Called { get; set; }
            public override ValidationResult Validate()
            {
                Called = true;
                return new ValidationResult();
            }
        }
        [UnitTest]
        public void ExecuteShouldCallValidate()
        {
            TestExecutionRequest to = new TestExecutionRequest(null, "TestMethod", "json");
            Expect.IsFalse(to.Called);
            to.Execute();
            Expect.IsTrue(to.Called);
        }

        [UnitTest]
        public void MissingClassShouldReturnClassNotSpecifiedResult()
        {
            ExecutionRequest er = new ExecutionRequest(null, "TestMethod", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailure.ToList().Contains(ValidationFailures.ClassNameNotSpecified));
            OutFormat(result.Message);
        }

        class TestClass
        {
            public void Method(object param)
            {

            }

            public string ShouldWork()
            {
                return "Yay";
            }
        }

        [UnitTest]
        public void MissingMethodShouldReturnMethodNotSpecified()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailure.ToList().Contains(ValidationFailures.MethodNameNotSpecified));
            OutFormat(result.Message);
        }

        [UnitTest]
        public void UnregisteredClassShoudReturnClassNotRegistered()
        {
            //ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "TestMethod", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailure.ToList().Contains(ValidationFailures.ClassNotRegistered));
            OutFormat(result.Message);
        }

        [UnitTest]
        public void MethodNotFoundShouldBeReturned()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "TestMethod", "json");
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailure.ToList().Contains(ValidationFailures.MethodNotFound));
            OutFormat(result.Message);
        }

        [UnitTest]
        public void ParameterCountMismatchShouldBeReturned()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "Method", "json");
            er.Parameters = new object[] { new { }, new { } };
            ValidationResult result = er.Validate();
            Expect.IsFalse(result.Success);
            Expect.IsTrue(result.ValidationFailure.ToList().Contains(ValidationFailures.ParameterCountMismatch));
            OutFormat(result.Message);
        }

        [UnitTest]
        public void ExecuteShouldSucceed()
        {
            ServiceProxySystem.Register<TestClass>();
            ExecutionRequest er = new ExecutionRequest("TestClass", "ShouldWork", "json");
            ValidationResult result = er.Validate();            
            Expect.IsTrue(result.Success);
            er.Execute();
            Expect.IsTrue(er.Result.Equals("Yay"));
            OutFormat(er.Result.ToString());
        }
    }

}
