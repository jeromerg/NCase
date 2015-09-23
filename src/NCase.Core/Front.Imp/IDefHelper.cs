using System;
using NCase.Front.Api;

namespace NCase.Front.Imp
{
    public interface IDefHelper
    {
        ISet Cases { get; }
        IDisposable Define();
        void Begin();
        void End();
        void Ref();
    }
}