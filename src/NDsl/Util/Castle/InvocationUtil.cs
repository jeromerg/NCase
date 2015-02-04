using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using NCase.Util.Quality;

namespace NDsl.Util.Castle
{
    public class InvocationUtil
    {
        [CanBeNull]
        public static PropertyCallKey GetCallKeyFromGetter(IInvocation invocation)
        {
            PropertyInfo propertyInfo = GetPropertyFromGetter(invocation);
            
            return propertyInfo == null 
                        ? null 
                        : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyCallKey GetCallKeyFromSetter(IInvocation invocation)
        {
            PropertyInfo propertyInfo = GetPropertyFromSetter(invocation);
            
            return propertyInfo == null 
                        ? null 
                        : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyInfo GetPropertyFromGetter(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return invocation.Proxy.GetType().GetProperties().FirstOrDefault(p => p.GetGetMethod() == method);
        }
        [CanBeNull]
        public static PropertyInfo GetPropertyFromSetter(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllImplementingAndExtendingTypes(invocation.Proxy.GetType()).SelectMany(t => t.GetProperties()).FirstOrDefault(p => p.GetSetMethod() == method);
        }

        private static IEnumerable<Type> GetAllImplementingAndExtendingTypes(Type type)
        {
            yield return type;

            foreach (var interf in type.GetInterfaces())
                yield return interf;

            Type parent;
            while( (parent = type.BaseType) != null)
                yield return parent;
        }
    }
}