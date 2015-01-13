using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NTestCase.Util.Typ
{
    public class TargetTypeNotResolvedException : Exception
    {
        private readonly System.Type mType;
        private readonly List<TargetTypeInfo> mCandidateInfos = new List<TargetTypeInfo>();

        public TargetTypeNotResolvedException(System.Type type, IEnumerable<TargetTypeInfo> candidateStatuses)
        {
            mType = type;
            mCandidateInfos.AddRange(candidateStatuses);
        }

        protected TargetTypeNotResolvedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                var b = new StringBuilder();

                b.AppendFormat("No unambiguous Type to assign type {0} to. Candidates:\n", mType.Name);
                foreach (var candidateInfo in mCandidateInfos)
                {
                    b.AppendFormat("{0}: {1}", candidateInfo.Type, candidateInfo.Status);
                }
                return b.ToString();
            }
        }
    }
}
