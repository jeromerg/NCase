using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    public static class DefExtensions
    {
        [NotNull, ItemNotNull]
        public static IEnumerable<Case> Cases([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId, Definer> setDef)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Api.Services.GetService<IGetCasesSvc>().GetCases(setDef.Api.Model);
        }

        [NotNull]
        public static string PrintDefinition([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId, Definer> setDef,
                                             bool isFileInfo = false,
                                             bool isRecursive = false)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Api.Services.GetService<IPrintDefSvc>().PrintDef(setDef.Api.Model, isFileInfo, isRecursive);
        }

        [NotNull]
        public static string PrintCasesAsTable([NotNull] this SetDefBase<ISetDefModel<ISetDefId>, ISetDefId, Definer> setDef,
                                               bool isRecursive = false)
        {
            if (setDef == null) throw new ArgumentNullException("setDef");

            return setDef.Api.Services.GetService<IPrintCaseTableSvc>().Perform(setDef.Api.Model, isRecursive);
        }
    }
}