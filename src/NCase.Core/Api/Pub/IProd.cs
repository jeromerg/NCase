using System;
using NCase.Api.Dev.Core;

namespace NCase.Api.Pub
{
    public interface IProd : IDef
    {
        IDisposable Define();
        void Ref();
    }
}