using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NDsl.Api.Core;


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