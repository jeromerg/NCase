using System;

namespace NCase.Front.Api
{
    public interface ITree : IDef
    {
        IDisposable Define();
        void Ref();
    }
}