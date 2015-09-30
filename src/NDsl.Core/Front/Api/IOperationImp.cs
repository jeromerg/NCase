using System.Diagnostics.CodeAnalysis;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public interface IOperationImp<TArtefact, TOperation, TArtefactImp, TResult>
        : IOperationVisitorClass
        where TOperation : IOp<TArtefact, TResult>
        where TArtefactImp : IArtefactImp
        where TArtefact : IArtefact
    {
        TResult Perform(IOperationDirector director, TOperation operation, TArtefactImp artefactImp);
    }
}