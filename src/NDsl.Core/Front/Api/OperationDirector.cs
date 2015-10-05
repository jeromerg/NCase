using System;
using System.Collections.Generic;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public class OperationDirector : IOperationDirector
    {
        private readonly IOperationVisitMapper mVisitMapper;

        public OperationDirector(IEnumerable<IOperationVisitorClass> visitorEnumerable)
        {
            mVisitMapper = new OperationVisitMapper(visitorEnumerable);
        }

        public OperationDirector(IOperationVisitMapper visitMapper)
        {
            mVisitMapper = visitMapper;
        }

        public TResult Perform<TArtefact, TResult>(IOp<TArtefact, TResult> operation, IArtefactImp artefactImp)
            where TArtefact : IArtefact
        {
            if (ReferenceEquals(operation, null))
                throw new ArgumentNullException("operation");

            if (ReferenceEquals(artefactImp, null))
                throw new ArgumentNullException("artefactImp");

            Func<IOperationDirector, IOp, IArtefactImp, object> visitAction;
            //try
            //{
            visitAction = mVisitMapper.GetVisitDelegate(operation, artefactImp);
            //}
            //catch (TargetTypeNotResolvedException e)
            //{
            //    throw new VisitorNotFoundException(GetType(), e);
            //}
            return (TResult) visitAction(this, operation, artefactImp);
        }
    }
}