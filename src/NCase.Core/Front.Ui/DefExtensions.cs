using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class DefExtensions
    {
        public static CaseEnumerable Cases(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef)
        {
            return setDef.Zapi.Services.GetService<IGetCases>().Perform(setDef.Zapi.Model);
        }

        public static string PrintDefinition(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                             bool isFileInfo = false,
                                             bool isRecursive = false)
        {
            return setDef.Zapi.Services.GetService<IPrintDef>().Perform(setDef.Zapi.Model, isFileInfo, isRecursive);
        }

        public static string PrintCasesAsTable(this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                               bool isRecursive = false)
        {
            return setDef.Zapi.Services.GetService<IPrintCaseTable>().Perform(setDef.Zapi.Model, isRecursive);
        }
    }
}