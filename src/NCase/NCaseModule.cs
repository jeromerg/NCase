using System.Reflection;
using Autofac;
using NCase.Front.Imp;
using NCase.Front.Ui;
using NDsl;
using Module = Autofac.Module;

namespace NCase
{
    public class NCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // modules
            builder.RegisterModule(new NDslCoreModule(new[] {Assembly.GetExecutingAssembly()}));
            builder.RegisterModule<NDslRecPlayModule>();
            builder.RegisterModule<NCaseCoreModule>();
            builder.RegisterModule<NCaseInterfaceRecPlayModule>();
            builder.RegisterModule<NCaseTreeModule>();
            builder.RegisterModule<NCaseProdModule>();
            builder.RegisterModule<NCasePairwiseModule>();

            // case builder 
            builder.RegisterType<Builder>().As<IBuilder>().InstancePerDependency();
        }
    }
}