using JetBrains.Annotations;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public interface IOperationDirector
    {
        TResult Perform<TArtefact, TResult>([NotNull] IOp<TArtefact, TResult> operation, [NotNull] IArtefactImp artefactImp)
            where TArtefact : IArtefact;
    }
}