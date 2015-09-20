using System.Collections.Generic;
using NDsl.Api.Dev.Core;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IParserGenerator
    {
        IEnumerable<ICase> ParseAndGenerate(IDef def, ITokenReader tokenReader);
    }
}
