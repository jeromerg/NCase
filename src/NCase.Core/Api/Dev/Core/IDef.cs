using System.Collections.Generic;

namespace NCase.Api.Dev.Core
{
    /// <summary>Case Set Definition</summary>
    public interface IDef
    {
        IEnumerable<ICase> Cases { get; }
    }
}