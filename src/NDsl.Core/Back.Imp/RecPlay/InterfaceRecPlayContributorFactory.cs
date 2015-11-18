using System;
using System.Linq;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayContributorFactory : IInterfaceRecPlayContributorFactory
    {
        [NotNull] private readonly ProxyGenerator mProxyGenerator;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public InterfaceRecPlayContributorFactory(
            [NotNull] ProxyGenerator proxyGenerator,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            mProxyGenerator = proxyGenerator;
            mCodeLocationUtil = codeLocationUtil;
        }

        [NotNull]
        public T CreateContributor<T>([NotNull] ITokenWriter tokenWriter, [NotNull] string contributorName)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (contributorName == null) throw new ArgumentNullException("contributorName");

            var interceptor = new InterfaceRecPlayInterceptor(tokenWriter, contributorName, mCodeLocationUtil);

            Type[] interfaces = typeof (T).GetInterfaces()
                                          .Where(i => (i.IsPublic || i.IsNestedPublic) && !i.IsImport).ToArray();

            return (T) mProxyGenerator.CreateInterfaceProxyWithoutTarget(typeof (T),
                                                                         interfaces,
                                                                         ProxyGenerationOptions.Default,
                                                                         interceptor);
        }
    }
}