﻿using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayNode : INode
    {
        [CanBeNull] object PropertyValue { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        string ContributorName { get; }

        bool IsReplay { get; set; }
    }
}