using System.Diagnostics;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Util
{
    public interface IStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}