using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IGetCasesSvc
        : IService<ISetDefModel<ISetDefId>>
    {
        [NotNull, ItemNotNull]
        IEnumerable<Ui.Case> GetCases([NotNull] ISetDefModel<ISetDefId> setDefModel);
    }
}