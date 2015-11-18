using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public class BuilderFactory : IBuilderFactory
    {
        [NotNull] private readonly ITokenStreamFactory mTokenStreamFactory;
        [NotNull] private readonly IServiceSet<ICaseBuilderModel> mServices;

        public BuilderFactory([NotNull] ITokenStreamFactory tokenStreamFactory, [NotNull] IServiceSet<ICaseBuilderModel> services)
        {
            mTokenStreamFactory = tokenStreamFactory;
            mServices = services;
        }

        [NotNull]
        public Api.CaseBuilder Create()
        {
            return new CaseBuilder(mTokenStreamFactory.Create(), mServices);
        }
    }
}