using System;
using NCase.Front.Api;

namespace NCase.Front.Imp.Op
{
    public interface IOperationVisitMapper
    {
        Func<IOperationDirector, IOp, IArtefactImp, object>
            GetVisitDelegate(IOp operation, IArtefactImp artefactImp);
    }
}