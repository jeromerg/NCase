using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NDsl.Api.Core;

namespace NCase.Front.Imp
{
    public interface IDefHelperFactory : IDefFactory
    {
        DefHelper<TDefId, TDef> CreateDefHelper<TDefId, TDef>([NotNull] TDefId defId,
                                                              [NotNull] string defName,
                                                              [NotNull] ITokenReaderWriter tokenReaderWriter)
            where TDefId : IDefId;
    }
}