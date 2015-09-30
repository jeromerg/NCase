using System;

namespace NDsl.Front.Api
{
    public interface IDef : IArtefact
    {
        IDisposable Define();
        void Ref();
    }

    /// <summary>Case Set Definition</summary>
    public interface IDef<out TDef> : IArtefact<TDef>, IDef
        where TDef : IDef<TDef>
    {
    }
}