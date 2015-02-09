﻿using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    public interface IDumpDir : IDirector<INode, IDumpDir>
    {
        void Indent();
        void Dedent();
        [StringFormatMethod("format")]
        void AddText(string format, params object[] formatArgs);
    }
}
