using Castle.Core.Internal;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class ReplayVisitors
        : IReplayVisitor<IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IReplayDirector dir, [NotNull] IInterfaceRecPlayNode node, bool isReplay)
        {
            node.SetReplay(isReplay);
            node.Children.ForEach(c => dir.Visit(c, isReplay));
        }
    }
}