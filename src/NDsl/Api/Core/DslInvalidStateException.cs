﻿using System;
using System.Runtime.Serialization;

namespace NDsl.Api.Core
{
    public class DslInvalidStateException : Exception
    {
        public DslInvalidStateException(string message) : base(message)
        {
        }

        protected DslInvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}