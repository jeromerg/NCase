using System;
using NDsl.Front.Api;

namespace NDsl.Front.Imp.Op
{
    public interface IOperationVisitMapper
    {
        Func<IOperationDirector, IOp, IArtefactImp, object>
            GetVisitDelegate(IOp operation, IArtefactImp artefactImp);
    }
}