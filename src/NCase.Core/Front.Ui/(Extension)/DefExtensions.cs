using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases(this ISetDef<ISetDefApi<ISetDefApi<ISetDefApi, ISetDefId>, ISetDefId>> setDef)
        {
            /*Error	2	
             * The type 'NCase.Front.Imp.Op.IGetCases' cannot be used as type parameter 'TTool' in the 
             * generic type or method 'NDsl.All.IToolBox<TClass>.GetTool<TTool>()'. 
             * There is no implicit reference conversion from 'NCase.Front.Imp.Op.IGetCases' to 
             * 'NDsl.All.ITool<NCase.Front.Api.ISetDefApi>'.	
             * */
            return setDef.Api.ToolBox.GetTool<IGetCases>().Perform(setDef.Api);
        }

        public static string PrintDef(this ISetDef<ISetDefApi<ISetDefApi<ISetDefApi, ISetDefId>, ISetDefId>> setDef,
                                      bool isFileInfo = false,
                                      bool isRecursive = false)
        {
            return setDef.Api.ToolBox.GetTool<IPrintDef>().Perform(setDef.Api, isFileInfo, isRecursive);
        }

        public static string PrintTable(this ISetDef<ISetDefApi<ISetDefApi<ISetDefApi, ISetDefId>, ISetDefId>> setDef,
                                        bool isRecursive = false)
        {
            return setDef.Api.ToolBox.GetTool<IPrintTable>().Perform(setDef.Api, isRecursive);
        }
    }
}