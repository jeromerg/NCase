using System;
using System.Linq;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayContributorFactory : IInterfaceRecPlayContributorFactory
    {
        [NotNull] private readonly ProxyGenerator mProxyGenerator;
        [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;

        public InterfaceRecPlayContributorFactory(
            [NotNull] ProxyGenerator proxyGenerator,
            [NotNull] ICodeLocationFactory codeLocationFactory)
        {
            mProxyGenerator = proxyGenerator;
            mCodeLocationFactory = codeLocationFactory;
        }

        /// <exception cref="TypeCannotBeContributorException"/>
        /// <exception cref="NoParameterlessConstructorException"/>
        [NotNull]
        public T CreateContributor<T>([NotNull] ITokenWriter tokenWriter, [NotNull] string contributorName, bool setupUndefinedProperties)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (contributorName == null) throw new ArgumentNullException("contributorName");

            var interceptor = new InterfaceRecPlayInterceptor(tokenWriter, contributorName, mCodeLocationFactory, setupUndefinedProperties);

            Type mockType = typeof (T);

            // ReSharper disable once PossibleNullReferenceException
            Type[] interfaces = mockType
                .GetInterfaces()
                .Where(i => (i.IsPublic || i.IsNestedPublic) && !i.IsImport)
                .ToArray();


            var proxyOptions = new ProxyGenerationOptions {Hook = new ProxyMethodHook()};

            if (mockType.IsInterface)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return (T) mProxyGenerator.CreateInterfaceProxyWithoutTarget(mockType,
                                                                             interfaces,
                                                                             ProxyGenerationOptions.Default,
                                                                             interceptor);
            }
            else
            {
                try
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return (T)mProxyGenerator.CreateClassProxy(mockType, interfaces, proxyOptions, new object[0], interceptor);
                }
                catch (TypeLoadException)
                {
                    throw new TypeCannotBeContributorException(mockType);
                }
                catch (MissingMethodException)
                {
                    throw new NoParameterlessConstructorException(mockType);
                }
            }
        }
    }
}