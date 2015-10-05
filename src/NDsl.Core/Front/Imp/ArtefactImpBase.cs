using System;
using JetBrains.Annotations;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class ArtefactImpBase<TArtefact, TArtefactImp> : IArtefact<TArtefact>, IArtefactImp
        where TArtefact : IArtefact<TArtefact>
        where TArtefactImp : IArtefactImp
    {
        [NotNull] private readonly IOperationDirector mOperationDirector;

        protected ArtefactImpBase([NotNull] IOperationDirector operationDirector)
        {
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mOperationDirector = operationDirector;
        }

        [NotNull] protected abstract TArtefactImp ThisDefImpl { get; }

        public TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TArtefact, TResult>
        {
            return mOperationDirector.Perform(operation, ThisDefImpl);
        }
    }
}