using NCase.Api.Pub;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Api.Dev.Core
{
    public interface IDefHelperFactory : IDefFactory
    {
        DefHelper<TDef> CreateDefHelper<TDef>([NotNull] TDef def,
                                              [NotNull] string defName,
                                              [NotNull] ITokenReaderWriter tokenReaderWriter)
            where TDef : IDef;
    }
}