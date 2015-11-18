using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IInterfaceRecPlayNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IInterfaceRecPlayNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            yield return new List<INode> {node};
        }
    }
}