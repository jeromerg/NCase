using System;

namespace NCase.Front.Api
{
    public interface ITree : IDef<ITree>
    {
        IDisposable Define();
        void Ref();
    }
}