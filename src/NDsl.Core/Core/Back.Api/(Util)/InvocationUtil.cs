using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public class InvocationUtil
    {
        [CanBeNull]
        public static PropertyCallKey TryGetPropertyCallKeyFromGetter(IInvocation invocation)
        {
            PropertyInfo propertyInfo = TryGetPropertyInfoFromGetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyCallKey TryGetPropertyCallKeyFromSetter(IInvocation invocation)
        {
            PropertyInfo propertyInfo = TryGetPropertyInfoFromSetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromGetter(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetGetMethod() == method);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromSetter(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetSetMethod() == method);
        }

        private static IEnumerable<PropertyInfo> GetAllImplementedProperties(Type type)
        {
            return GetAllImplementingAndExtendingTypes(type).SelectMany(t => t.GetProperties());
        }

        private static IEnumerable<Type> GetAllImplementingAndExtendingTypes(Type type)
        {
            yield return type;

            foreach (Type interf in type.GetInterfaces())
                yield return interf;

            Type parent;
            while ((parent = type.BaseType) != null)
                yield return parent;
        }
    }
}