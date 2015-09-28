using JetBrains.Annotations;
using NCase.Front.Api;

namespace NCase.Front.Imp.Op
{
    public interface IOperationDirector
    {
        TResult Perform<TArtefact, TResult>([NotNull] IOp<TArtefact, TResult> operation, [NotNull] IArtefactImp artefactImp);
    }
}