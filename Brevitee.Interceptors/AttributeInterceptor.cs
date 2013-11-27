using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using System.Reflection;
using Brevitee;

namespace Brevitee.Interceptors
{
    public abstract class AttributeInterceptor<T>: IInterceptor where T: Attribute, new()
    {
        /// <summary>
        /// When implemented in a derived class, should examine the invocation and attribute
        /// instance and set the CanProceed property as appropriate.
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="attribute"></param>
        public abstract void Examine(IInvocation invocation, T attribute);

        public bool CanProceed
        {
            get;
            set;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            T attr;
            if (invocation.Method.HasCustomAttributeOfType<T>(out attr))
            {
                Examine(invocation, attr);
                if (CanProceed)
                {
                    invocation.Proceed();
                }
            }
        }

        #endregion
    }
}
