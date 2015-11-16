using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public class BuilderFactory : IBuilderFactory
    {
        private readonly ITokenStreamFactory mTokenStreamFactory;
        private readonly IServiceSet<IBuilderModel> mServices;

        public BuilderFactory([NotNull] ITokenStreamFactory tokenStreamFactory, [NotNull] IServiceSet<IBuilderModel> services)
        {
            if (tokenStreamFactory == null) throw new ArgumentNullException("tokenStreamFactory");
            if (services == null) throw new ArgumentNullException("services");
            mTokenStreamFactory = tokenStreamFactory;
            mServices = services;
        }

        public IBuilder Create()
        {
            return new Builder(mTokenStreamFactory.Create(), mServices);
        }
    }
}