using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class DefExtensions
    {
        public static CaseEnumerable Cases(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef)
        {
            return setDef.Api.Services.GetService<IGetCases>().Perform(setDef.Api.Model);
        }

        public static string PrintDefinition(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                             bool isFileInfo = false,
                                             bool isRecursive = false)
        {
            return setDef.Api.Services.GetService<IPrintDef>().Perform(setDef.Api.Model, isFileInfo, isRecursive);
        }

        public static string PrintCasesAsTable(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                               bool isRecursive = false)
        {
            return setDef.Api.Services.GetService<IPrintTable>().Perform(setDef.Api.Model, isRecursive);
        }
    }
}