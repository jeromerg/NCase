using System.Collections.Generic;

namespace NCase.Api.Dev.Core
{
    public interface ICaseSet
    {
        IEnumerable<ICase> Cases { get; }
    }
}