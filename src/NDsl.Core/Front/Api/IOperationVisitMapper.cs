using System;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public interface IOperationVisitMapper
    {
        Func<IOperationDirector, IOp, IArtefactImp, object>
            GetVisitDelegate(IOp operation, IArtefactImp artefactImp);
    }
}