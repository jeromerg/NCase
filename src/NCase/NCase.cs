using Autofac;
using NCase.Front.Api;

namespace NCase
{
    public static class NCase
    {
        public static IBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<NCaseModule>();
            IContainer container = containerBuilder.Build();
            return container.Resolve<IBuilder>();
        }
    }
}