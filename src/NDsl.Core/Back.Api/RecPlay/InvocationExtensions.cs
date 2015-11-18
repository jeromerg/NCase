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
        public static PropertyCallKey TryGetPropertyCallKeyFromGetter([NotNull] this IInvocation invocation)
        {
            if (invocation == null) throw new ArgumentNullException("invocation");

            PropertyInfo propertyInfo = TryGetPropertyInfoFromGetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyCallKey TryGetPropertyCallKeyFromSetter([NotNull] this IInvocation invocation)
        {
            if (invocation == null) throw new ArgumentNullException("invocation");

            PropertyInfo propertyInfo = TryGetPropertyInfoFromSetter(invocation);

            return propertyInfo == null
                       ? null
                       : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromGetter([NotNull] this IInvocation invocation)
        {
            if (invocation == null) throw new ArgumentNullException("invocation");

            MethodInfo method = invocation.Method;
            
            // ReSharper disable once PossibleNullReferenceException
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetGetMethod() == method);
        }

        [CanBeNull]
        public static PropertyInfo TryGetPropertyInfoFromSetter([NotNull] this IInvocation invocation)
        {
            if (invocation == null) throw new ArgumentNullException("invocation");

            MethodInfo method = invocation.Method;

            // ReSharper disable once PossibleNullReferenceException
            return GetAllImplementedProperties(invocation.Proxy.GetType()).FirstOrDefault(p => p.GetSetMethod() == method);
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<PropertyInfo> GetAllImplementedProperties([NotNull] this Type type)
        {
            return GetAllImplementingAndExtendingTypes(type).SelectMany(t => t.GetProperties());
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<Type> GetAllImplementingAndExtendingTypes([NotNull] this Type type)
        {
            foreach (Type interf in type.GetInterfaces())
                yield return interf;


            for (Type t = type; t != null; t = t.BaseType)
                yield return t;
        }
    }
}