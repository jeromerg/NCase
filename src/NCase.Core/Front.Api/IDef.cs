using System.Collections.Generic;

namespace NCase.Front.Api
{
    /// <summary>Case Set Definition</summary>
    public interface IDef<TDef>
    {
        IEnumerable<ICase> Cases { get; }
        TResult Get<TFunc, TResult>(TFunc func) where TFunc : IFuncSettings<TDef, TResult>;
    }
}