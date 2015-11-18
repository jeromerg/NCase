using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class DefExtensions
    {
        [NotNull, ItemNotNull] 
        public static IEnumerable<Case> Cases([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Zapi.Services.GetService<IGetCasesSvc>().GetCases(setDef.Zapi.Model);
        }

        [NotNull] 
        public static string PrintDefinition([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                             bool isFileInfo = false,
                                             bool isRecursive = false)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Zapi.Services.GetService<IPrintDefSvc>().PrintDef(setDef.Zapi.Model, isFileInfo, isRecursive);
        }

        [NotNull] 
        public static string PrintCasesAsTable([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId> setDef,
                                               bool isRecursive = false)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Zapi.Services.GetService<IPrintCaseTableSvc>().Perform(setDef.Zapi.Model, isRecursive);
        }
    }
}