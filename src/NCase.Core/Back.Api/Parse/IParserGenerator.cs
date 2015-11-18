using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IParserGenerator
    {
        [NotNull]
        INode Parse([NotNull] IDefId def, [NotNull] ITokenReader tokenReader);

        [NotNull, ItemNotNull]
        IEnumerable<List<INode>> Generate([NotNull] INode caseSetNode, [NotNull] GenerateOptions options);
    }
}