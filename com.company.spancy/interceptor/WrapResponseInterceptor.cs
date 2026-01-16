using AopAlliance.Intercept;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.interceptor
{
    public class WrapResponseInterceptor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            return new Response(invocation.Proceed());
            //dynamic result = invocation.Proceed();
            //if (invocation.Method.ReturnType != typeof(void) && result != null && !result.GetType().IsPrimitive)
            //{
            //    return new Response((invocation.This as dynamic).Mapper.Map(result));
            //} 
            //else
            //{
            //    return new Response(result);
            //}
        }
    }
}
