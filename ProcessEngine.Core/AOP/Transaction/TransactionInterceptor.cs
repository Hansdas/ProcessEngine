using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ProcessEngine.Core.AOP.Transaction
{
    /// <summary>
    /// 事务拦截特性，
    /// </summary>
    public class TransactionInterceptor : ITransactionInterceptor
    {

        public void Intercept(TransactionAttribute attribute, IInvocation invocation)
        {
            if (attribute != null)
            {
                //事务过期和级别信息
                TransactionOptions transactionOptions = new TransactionOptions()
                {
                    IsolationLevel = Enum.Parse<IsolationLevel>(attribute.Level.GetValue().ToString()),
                    Timeout = attribute.TimeoutSecond.HasValue ? new TimeSpan(0, 0, attribute.TimeoutSecond.Value) : new TimeSpan(0, 0, 10)
                };
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    try
                    {
                        invocation.Proceed();
                        transactionScope.Complete();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        //transactionScope.Dispose();
                    }
                }
            }
        }
    }
}
