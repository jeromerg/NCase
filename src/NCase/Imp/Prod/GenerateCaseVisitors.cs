using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Prod;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Prod
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IProdNode>
        , IGenerateCaseVisitor<IRefNode<IProdNode>>
        , IGenerateCaseVisitor<ProdDimNode>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector dir, IProdNode node)
        {
            List<INode> operands = node.Children.ToList();

            if(operands.Count == 0)
                yield break;

            foreach (var pause in ProduceCardinalProductRecursively(dir, operands, 0))
                yield return Pause.Now;
        }

        private IEnumerable<Pause> ProduceCardinalProductRecursively(IGenerateCaseDirector dir, List<INode> operands, int operandIndex)
        {
            bool isLastOperand = operandIndex == operands.Count - 1;
            INode currentOperand = operands[operandIndex];

            foreach (var pause in dir.Visit(currentOperand))
            {
                if (isLastOperand)
                {
                    yield return Pause.Now; // end of recursion here
                }
                else
                {
                    // continue recursion
                    foreach (var pause1 in ProduceCardinalProductRecursively(dir, operands, operandIndex + 1))
                        yield return Pause.Now;
                }
            }

        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IRefNode<IProdNode> node)
        {
            foreach (Pause pause in director.Visit(node.Reference))
                yield return Pause.Now;
        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, ProdDimNode node)
        {
            foreach(var child in node.Children)
                foreach (var pause in director.Visit(child))
                    yield return Pause.Now;
        }
    }
}