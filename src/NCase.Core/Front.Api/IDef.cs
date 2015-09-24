using System.Collections.Generic;

namespace NCase.Front.Api
{
    /// <summary>Case Set Definition</summary>
    public interface IDef
    {
        IEnumerable<ICase> Cases { get; }

        //T Get<T>(IFunc<IDef, T> function);
    }
}