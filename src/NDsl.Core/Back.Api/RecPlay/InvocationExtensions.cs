using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using JetBrains.Annotations;

namespace NDsl.Back.Api.RecPlay
{
    public static class InvocationExtensions
    {
        [CanBeNull]
        public static PropertyCallKey TryGetPropertyCallKeyFromGetter(this IInvocation invocation)
        {
            PropertyInfo propertyInfo = TryGetPropertyInfoFromGetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyCallKey TryGetPropertyCallKeyFromSetter(this IInvocation invocation)
        {
            PropertyInfo propertyInfo = TryGetPropertyInfoFromSetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromGetter(this IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetGetMethod() == method);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromSetter(this IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetSetMethod() == method);
        }

        private static IEnumerable<PropertyInfo> GetAllImplementedProperties(this Type type)
        {
            return GetAllImplementingAndExtendingTypes(type).SelectMany(t => t.GetProperties());
        }

        private static IEnumerable<Type> GetAllImplementingAndExtendingTypes(this Type type)
        {
            foreach (Type interf in type.GetInterfaces())
                yield return interf;


            for (Type t = type; t != null; t = t.BaseType)
                yield return t;
        }
    }
}