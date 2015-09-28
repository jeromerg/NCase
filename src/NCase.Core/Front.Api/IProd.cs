using System;

namespace NCase.Front.Api
{
    public interface IProd : IDef<IProd>
    {
        IDisposable Define();
        void Ref();
    }
}