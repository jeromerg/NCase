using Autofac;
using NCase.Api.Pub;

namespace NCase
{
    public static class NCase
    {
        public static IBuilder CreateBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            IContainer container = builder.Build();
            return container.Resolve<IBuilder>();
        }
    }
}