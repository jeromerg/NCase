using System.Diagnostics;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public interface IStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}