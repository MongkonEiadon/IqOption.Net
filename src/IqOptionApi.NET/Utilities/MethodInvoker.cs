using System;
using System.Reflection;

namespace IqOptionApi.Utilities
{
    internal class MethodInvoker<T> where T : Attribute
    {
        public MethodInvoker(T attribute, MethodInfo targetMethod)
        {
            Attribute = attribute;
            TargetMethod = targetMethod;
        }

        public T Attribute { get; }
        public MethodInfo TargetMethod { get; }
    }
}