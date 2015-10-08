using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases(this ISetDef<ISetDefApi<ISetDefApi, ISetDefId>> setDef)
        {
            return setDef.Api.Toolbox.GetTool<IGetCases>().Perform(setDef.Api);
        }

        public static string PrintDef(this ISetDef<ISetDefApi<ISetDefApi, ISetDefId>> setDef,
                                      bool isFileInfo = false,
                                      bool isRecursive = false)
        {
            return setDef.Api.Toolbox.GetTool<IPrintDef>().Perform(setDef.Api, isFileInfo, isRecursive);
        }

        public static string PrintTable(this ISetDef<ISetDefApi<ISetDefApi, ISetDefId>> setDef,
                                      bool isFileInfo = false,
                                      bool isRecursive = false)
        {
            return setDef.Api.Toolbox.GetTool<IPrintTable>().Perform(setDef.Api, isRecursive);
        }
    }
}