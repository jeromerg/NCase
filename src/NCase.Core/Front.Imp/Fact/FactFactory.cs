using System;
using JetBrains.Annotations;
using NCase.Front.Api.Fact;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Fact
{
    public class FactFactory : IFactFactory
    {
        [NotNull] private readonly IServices<IFactModel> mServices;

        public FactFactory([NotNull] IServices<IFactModel> services)
        {
            if (services == null) throw new ArgumentNullException("services");
            mServices = services;
        }

        public IFact Create(INode fact)
        {
            return new Fact(fact, mServices);
        }
    }
}