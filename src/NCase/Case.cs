using Autofac;
using NCase.Api;

namespace NCase
{
    public static class Case
    {
        public static ICaseBuilder GetBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            IContainer container = builder.Build();
            return container.Resolve<ICaseBuilder>();
        }
    }
}
