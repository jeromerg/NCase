using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ISetDef : IDef
    {
        [NotNull] new ISetDefApi Api { get; }
        [NotNull] ISetDefId Id { get; }
    }   
    
    public interface ISetDef<TDefId> : IDef
    {
        [NotNull] new ISetDefApi<TDefId> Api { get; }
        [NotNull] TDefId Id { get; }
    }
}