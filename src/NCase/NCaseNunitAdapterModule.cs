using Autofac;
using NCaseFramework.NunitAdapter.Front.Imp;

namespace NCaseFramework.Front.Ui
{
    public class NCaseNunitAdapterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ActAndAssert>().AsImplementedInterfaces();
        }
    }
}