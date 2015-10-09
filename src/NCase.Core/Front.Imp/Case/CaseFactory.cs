using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api.Case;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Case
{
    public class CaseFactory : ICaseFactory
    {
        [NotNull] private readonly IServices<ICaseModel> mServices;

        public CaseFactory([NotNull] IServices<ICaseModel> services)
        {
            if (services == null) throw new ArgumentNullException("services");
            mServices = services;
        }

        public ICase Create(IEnumerable<INode> factNodes)
        {
            return new Case(factNodes, mServices);
        }
    }
}