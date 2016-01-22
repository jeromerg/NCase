using System;
using JetBrains.Annotations;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    public static class BuilderExtensions
    {
        [NotNull]
        public static T NewDefinition<T>([NotNull] this CaseBuilder caseBuilder, [NotNull] string name)
            where T : DefBase<Definer>
        {
            if (caseBuilder == null) throw new ArgumentNullException("caseBuilder");
            if (name == null) throw new ArgumentNullException("name");

            var treeFactory = caseBuilder.Zapi.Services.GetService<IDefFactory<T>>();
            return treeFactory.Create(name, caseBuilder.Zapi.Model.TokenStream);
        }
    }
}