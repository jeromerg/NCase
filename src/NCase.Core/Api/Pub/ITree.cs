using System;

namespace NCase.Api.Pub
{
    public interface ITree : IDef
    {
        IDisposable Define();
        void Ref();
    }
}