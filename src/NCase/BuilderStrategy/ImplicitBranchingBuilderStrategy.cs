using System.Linq;
using NCase.Api.Dev;

namespace NCase.BuilderStrategy
{
    public class ImplicitBranchingBuilderStrategy : IBuilderStrategy
    {
        public void PlaceChild(INode<ITarget> parentCandidate, INode<ITarget> child)
        {
            if (parentCandidate.Children.Count == 0)
            {
                parentCandidate.Children.Add(child);
                return;
            }

            INode<ITarget> lastChild = parentCandidate.Children.Last();

            if (Equals(lastChild.TargetKey, child.TargetKey))
            {
                parentCandidate.Children.Add(child);
            }
            else
            {
                PlaceChild(lastChild, child); // recursion
            }
        }    

    }
}
