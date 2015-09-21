using System;
using NCase.Api.Dev.Core;

namespace NCase.Api.Pub
{
    public interface ITree : IDef
    {
        IDisposable Define();
        void Ref();
    }
}