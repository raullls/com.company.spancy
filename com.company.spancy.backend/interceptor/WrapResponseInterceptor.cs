using AopAlliance.Intercept;
using Nancy;

namespace com.company.spancy.backend.interceptor
{
    public class WrapResponseInterceptor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            return new Response(invocation.Proceed());
        }
    }
}