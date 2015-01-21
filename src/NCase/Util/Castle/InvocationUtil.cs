using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using NCase.Core.InterfaceContrib;
using NCase.Util.Quality;

namespace NCase.Util.Castle
{
    public class InvocationUtil
    {
        [CanBeNull]
        public static PropertyCallKey GetterToPropertyCallKey(IInvocation invocation)
        {
            PropertyInfo propertyInfo = GetterToProperty(invocation);
            
            return propertyInfo == null 
                        ? null 
                        : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyCallKey SetterToPropertyCallKey(IInvocation invocation)
        {
            PropertyInfo propertyInfo = SetterToProperty(invocation);
            
            return propertyInfo == null 
                        ? null 
                        : new PropertyCallKey(invocation, propertyInfo);
        }

        [CanBeNull]
        public static PropertyInfo GetterToProperty(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return invocation.Proxy.GetType().GetProperties().FirstOrDefault(p => p.GetGetMethod() == method);
        }
        [CanBeNull]
        public static PropertyInfo SetterToProperty(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            return GetAllTypes(invocation.Proxy.GetType()).SelectMany(t => t.GetProperties()).FirstOrDefault(p => p.GetSetMethod() == method);
        }

        private static IEnumerable<Type> GetAllTypes(Type type)
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