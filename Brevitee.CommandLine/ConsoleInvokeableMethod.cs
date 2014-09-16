using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Brevitee;

namespace Brevitee.CommandLine
{
    [Serializable]
    public class ConsoleInvokeableMethod
    {
        public ConsoleInvokeableMethod(MethodInfo method)
            : this(method, null)
        {
        }
        
        public ConsoleInvokeableMethod(MethodInfo method, Attribute actionInfo)
        {
            Method = method;
            Attribute = actionInfo;
        }

        public ConsoleInvokeableMethod(MethodInfo method, Attribute actionInfo, object provider, string switchValue = "")
            : this(method, actionInfo)
        {
            this.Provider = provider;
            this.SwitchValue = switchValue;
        }

        /// <summary>
        /// Used to help build usage examples for /? 
        /// </summary>
        public string SwitchValue { get; internal set; }
        public MethodInfo Method { get; internal set; }
        public object[] Parameters { get; set; }
        public object Provider { get; set; }

        public string Information
        {
            get
            {
                string info = Method.Name.PascalSplit(" ");
                if (Attribute != null)
                {
                    IInfoAttribute consoleAction = Attribute as IInfoAttribute;
                    if (consoleAction != null && !string.IsNullOrEmpty(consoleAction.Information))
                    {
                        info = consoleAction.Information;                        
                    }
                }

                return info;
            }
        }

        public Attribute Attribute { get; set; }

        public object Invoke()
        {
            IPreAndPostInvoke preAndPost = Attribute as IPreAndPostInvoke;
            MethodInfo pre = null;
            MethodInfo post = null;
            MethodInfo alwaysPost = null;
            Exception thrown = null;
            object result = null;

            if (preAndPost != null && Provider != null)
            {
                Type providerType = Provider.GetType();
                if (!string.IsNullOrEmpty(preAndPost.Before))
                {
                    pre = providerType.GetMethod(preAndPost.Before);
                }

                if (!string.IsNullOrEmpty(preAndPost.AfterSuccess))
                {
                    post = providerType.GetMethod(preAndPost.AfterSuccess);
                }

                if (!string.IsNullOrEmpty(preAndPost.AlwaysAfter))
                {
                    alwaysPost = providerType.GetMethod(preAndPost.AlwaysAfter);
                }
            }

            try
            {

                if (pre != null)
                {
                    pre.Invoke(Provider, null);
                }

                result = Method.Invoke(Provider, Parameters);

                if (post != null)
                {
                    post.Invoke(Provider, null);
                }
            }
            catch (Exception ex)
            {
                thrown = ex;
                if (thrown.InnerException != null)
                {
                    thrown = thrown.InnerException;
                }
            }

            if (alwaysPost != null)
            {
                try
                {
                    alwaysPost.Invoke(Provider, null);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }

                    StringBuilder message = new StringBuilder();
                    if (thrown != null)
                    {
                        message.AppendLine(thrown.Message);
                        message.AppendLine();
                        if (!string.IsNullOrEmpty(thrown.StackTrace))
                        {
                            message.AppendLine(thrown.StackTrace);
                        }
                    }

                    thrown = new Exception(message.ToString(), ex);
                }
            }

            if (thrown != null)
            {
                throw thrown;
            }

            return result;
        }
    }
}
