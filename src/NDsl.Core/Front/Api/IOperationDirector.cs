using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NDsl.Front.Imp.Op
{
    public interface IOperationDirector
    {
        TResult Perform<TArtefact, TResult>([NotNull] IOp<TArtefact, TResult> operation, [NotNull] IArtefactImp artefactImp)
            where TArtefact : IArtefact;
    }
}