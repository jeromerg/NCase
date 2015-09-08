﻿using System;
using System.Linq;
using Castle.DynamicProxy;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NVisitor.Common.Quality;

namespace NDsl.Imp.RecPlay
{
    public class InterfaceRecPlayContributorFactory : IInterfaceRecPlayContributorFactory
    {
        [NotNull] private readonly ProxyGenerator mProxyGenerator;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public InterfaceRecPlayContributorFactory(
            [NotNull] ProxyGenerator proxyGenerator, 
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (proxyGenerator == null) throw new ArgumentNullException("proxyGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mProxyGenerator = proxyGenerator;
            mCodeLocationUtil = codeLocationUtil;
        }

        public T CreateContributor<T>(ITokenWriter tokenWriter, string contributorName)
        {
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