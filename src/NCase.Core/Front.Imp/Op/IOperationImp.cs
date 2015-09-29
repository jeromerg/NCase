using System.Diagnostics.CodeAnalysis;
using NCase.Front.Api;

namespace NCase.Front.Imp.Op
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