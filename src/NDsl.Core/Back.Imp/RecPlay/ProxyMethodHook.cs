using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace NDsl.Back.Imp.RecPlay
{
    /// <summary>
    /// Default implementation skips methods of type Object/MarshalByRefObject/ContextBoundObject
    /// but want to intercept everything. TODO: do we want to intercept everything?
    /// </summary>
    internal class ProxyMethodHook : IProxyGenerationHook
    {
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return true;
        }

        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }
    }
}