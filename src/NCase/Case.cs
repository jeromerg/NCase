using Autofac;
using NCase.Api;

namespace NCase
{
    public static class Case
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
