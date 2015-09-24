using System;
using System.Collections.Generic;
using NCase.Front.Api;

namespace NCase.Front.Imp
{
    public interface IDefHelper
    {
        IEnumerable<ICase> Cases { get; }
        IDisposable Define();
        void Begin();
        void End();
        void Ref();
    }
}