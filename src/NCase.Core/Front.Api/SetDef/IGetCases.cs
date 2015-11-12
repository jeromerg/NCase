using System.Collections.Generic;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IGetCases
        : IService<ISetDefModel<ISetDefId>>
    {
        IEnumerable<Ui.Case> Perform(ISetDefModel<ISetDefId> setDefModel);
    }
}