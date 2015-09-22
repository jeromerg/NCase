using System;
using NCase.Api.Pub;

namespace NCase.Api.Dev.Core
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