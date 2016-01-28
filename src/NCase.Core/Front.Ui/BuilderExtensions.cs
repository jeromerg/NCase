using System;
using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    public static class BuilderExtensions
    {
        [NotNull]
        public static CombinationSet NewCombinationSet([NotNull] this CaseBuilder caseBuilder, [NotNull] string name, bool onlyPairwise = false)
        {
            if (caseBuilder == null) throw new ArgumentNullException("caseBuilder");
            if (name == null) throw new ArgumentNullException("name");

            var treeFactory = caseBuilder.Zapi.Services.GetService<IDefFactory<CombinationSet>>();

            CombinationSet combinationSet = treeFactory.Create(name, caseBuilder.Zapi.Model.TokenStream);
            combinationSet.OnlyPairwise = onlyPairwise;
            return combinationSet;
        }
    }
}