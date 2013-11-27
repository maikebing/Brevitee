using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using System.Web.Security;

namespace Brevitee.ServiceProxy
{
    public enum ValidationFailures
    {
        None,
        ClassNameNotSpecified,
        ClassNotRegistered,
        MethodNameNotSpecified,
        MethodNotFound,
        MethodNotProxied,
        ParameterCountMismatch,
        PermissionDenied
    }

    public class ValidationResult
    {
        ExecutionRequest _toValidate;
        public ValidationResult() { }
        public ValidationResult(ExecutionRequest request, string messageDelimiter = null)
        {
            this._toValidate = request;
            this.Delimiter = messageDelimiter ?? ",\r\n";
        }
        
        internal void Execute()
        {
            List<ValidationFailures> failures = new List<ValidationFailures>();
            List<string> messages = new List<string>();
            if (string.IsNullOrWhiteSpace(_toValidate.ClassName))
            {
                failures.Add(ValidationFailures.ClassNameNotSpecified);
                messages.Add("ClassName not specified");
            }

            if (_toValidate.TargetType == null)
            {
                failures.Add(ValidationFailures.ClassNotRegistered);
                messages.Add("Class {0} was not registered as a proxied service.  Call ServiceProxySystem.Register first."._Format(_toValidate.ClassName));
            }

            if (string.IsNullOrWhiteSpace(_toValidate.MethodName))
            {
                failures.Add(ValidationFailures.MethodNameNotSpecified);
                messages.Add("MethodName not specified");
            }

            if (_toValidate.TargetType != null && _toValidate.MethodInfo == null)
            {
                failures.Add(ValidationFailures.MethodNotFound);
                string message = "Method ({0}) was not found"._Format(_toValidate.MethodName);
                if (!failures.Contains(ValidationFailures.ClassNameNotSpecified))
                {
                    message = "{0} on class ({1})"._Format(message, _toValidate.ClassName);
                }
                messages.Add(message);
            }

            if (_toValidate.TargetType != null && 
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType<ExcludeAttribute>())
            {
                failures.Add(ValidationFailures.MethodNotProxied);
                messages.Add("The specified method has been explicitly excluded from being proxied: {0}"._Format(_toValidate.MethodName));
            }

            if (_toValidate.ParameterInfos != null && _toValidate.ParameterInfos.Length != _toValidate.Parameters.Length)
            {
                failures.Add(ValidationFailures.ParameterCountMismatch);
                messages.Add("Wrong number of parameters specified: expected ({0}), recieved ({1})"._Format(_toValidate.ParameterInfos.Length, _toValidate.Parameters.Length));
            }

            RoleRequired requiredMethodRoles;
            if (_toValidate.TargetType != null && 
                _toValidate.MethodInfo != null &&
                _toValidate.MethodInfo.HasCustomAttributeOfType<RoleRequired>(true, out requiredMethodRoles))
            {
                if (requiredMethodRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredMethodRoles);
                }
            }

            RoleRequired requiredClassRoles;
            if (_toValidate.TargetType != null &&
                _toValidate.TargetType.HasCustomAttributeOfType<RoleRequired>(true, out requiredClassRoles))
            {
                if (requiredClassRoles.Roles.Length > 0)
                {
                    CheckRoles(failures, messages, requiredClassRoles);
                }
            }

            ValidationFailure = failures.ToArray();
            Message = messages.ToArray().ToDelimited(s => s, Delimiter);
            this.Success = failures.Count == 0;
        }

        private static void CheckRoles(List<ValidationFailures> failures, List<string> messages, RoleRequired requiredRoles)
        {
            string user = UserUtil.GetCurrentUser(false);
            List<string> userRoles = new List<string>(Roles.GetRolesForUser(user));
            bool passed = false;
            for (int i = 0; i < requiredRoles.Roles.Length; i++)
            {
                string requiredRole = requiredRoles.Roles[i];
                if (userRoles.Contains(requiredRole))
                {
                    passed = true;
                    break;
                }
            }

            if (!passed)
            {
                failures.Add(ValidationFailures.PermissionDenied);
                messages.Add("Permission Denied");
            }
        }

        internal string Delimiter { get; set; }
        public bool Success { get; set; }
        public string Message { get; private set; }
        public ValidationFailures[] ValidationFailure { get; set; }
    }
}
