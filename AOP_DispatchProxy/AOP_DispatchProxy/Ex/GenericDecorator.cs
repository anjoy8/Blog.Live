using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AOP_DispatchProxy.Ex
{
    public class GenericDecorator : DispatchProxy,IInterceptor
    {
        public object TargetClass { get; set; }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("方法执行前");

            invocation.Proceed();

            Console.WriteLine("方法执行后"); 
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {

            Console.WriteLine("方法执行前");

            var result = targetMethod.Invoke(TargetClass, args);
            
            Console.WriteLine("方法执行后");

            return result;
        }
    }
}
