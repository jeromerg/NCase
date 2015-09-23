using NCase.Back.Api.Core;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Front.Imp
{
    public interface IDefHelperFactory : IDefFactory
    {
        DefHelper<TDefId> CreateDefHelper<TDefId>([NotNull] TDefId defId,
                                                  [NotNull] string defName,
                                                  [NotNull] ITokenReaderWriter tokenReaderWriter)
            where TDefId : IDefId;
    }
}