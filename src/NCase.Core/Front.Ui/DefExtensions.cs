using NCase.Back.Api.SetDef;
using NCase.Front.Api.SetDef;

namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases(this ISetDef<ISetDefModel<ISetDefId>, ISetDefId> setDef)
        {
            return setDef.Api.Services.GetTool<IGetCases>().Perform(setDef.Api.Model);
        }

        public static string PrintDef(this ISetDef<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                      bool isFileInfo = false,
                                      bool isRecursive = false)
        {
            return setDef.Api.Services.GetTool<IPrintDef>().Perform(setDef.Api.Model, isFileInfo, isRecursive);
        }

        public static string PrintTable(this ISetDef<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                        bool isRecursive = false)
        {
            return setDef.Api.Services.GetTool<IPrintTable>().Perform(setDef.Api.Model, isRecursive);
        }
    }
}