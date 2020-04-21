using Castle.DynamicProxy;
using ProcessEngine.Core.AOP.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProcessEngine.Core.AOP
{
    public class Interceptor : IInterceptor
    {
        private readonly ITransactionInterceptor _transactionInterceptor;
        public Interceptor()
        {

        }
        public Interceptor(ITransactionInterceptor transactionInterceptor)
        {
            _transactionInterceptor = transactionInterceptor;
        }
        public void Intercept(IInvocation invocation)
        {
            Attribute attribute = GetAttribute(invocation.MethodInvocationTarget ?? invocation.Method) as Attribute;
            if (attribute is TransactionAttribute)
                _transactionInterceptor.Intercept((TransactionAttribute)attribute, invocation);
            else
                invocation.Proceed();
        }
        private object GetAttribute(MethodInfo method)
        {
            object[] attributes = method.GetCustomAttributes(true);
            return attributes.FirstOrDefault();
        }
    }
}
