using System;

namespace NCase.Front.Api
{
    public interface IProd : IDef
    {
        IDisposable Define();
        void Ref();
    }
}