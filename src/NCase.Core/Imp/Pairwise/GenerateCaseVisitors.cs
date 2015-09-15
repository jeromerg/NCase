using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Pairwise
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IPairwiseNode>
        , IGenerateCaseVisitor<IRefNode<IPairwiseNode>>
        , IGenerateCaseVisitor<PairwiseDimNode>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector dir, IPairwiseNode node)
        {
            List<INode> operands = node.Children.ToList();

            if(operands.Count == 0)
                yield break;

        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IRefNode<IPairwiseNode> node)
        {
            foreach (Pause pause in director.Visit(node.Reference))
                yield return Pause.Now;
        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, PairwiseDimNode node)
        {
            foreach(var child in node.Children)
                foreach (var pause in director.Visit(child))
                    yield return Pause.Now;
        }
    }
}