using System;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api;
using NDsl.Api.Core;
using NVisitor.Api.FuncPair;

namespace NCase.Front.Imp
{
    public interface IFuncDirector<TArteFact, TResult> 
        : IFuncPairDirector<IFuncSettings<object, object>,TArteFact,IFuncDirector<TArteFact, TResult>,TResult>
    {
    }

    public class Pairwise : DefBase<PairwiseId, IPairwise>, IPairwise
    {
        #region inner types

        public class Factory : IDefFactory<IPairwise>
        {
            private readonly IDefHelperFactory mDefHelperFactory;

            public Factory([NotNull] IDefHelperFactory defHelperFactory)
            {
                if (defHelperFactory == null) throw new ArgumentNullException("defHelperFactory");
                mDefHelperFactory = defHelperFactory;
            }

            public IPairwise Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Pairwise(tokenReaderWriter, name, mDefHelperFactory);
            }
        }

        #endregion

        public Pairwise([NotNull] ITokenReaderWriter tokenReaderWriter,
                        [NotNull] string defName,
                        [NotNull] IDefHelperFactory defHelperFactory)
            : base(new PairwiseId(), defName, tokenReaderWriter, defHelperFactory)
        {
        }

        public TResult Get<TFunc, TResult>(TFunc func) where TFunc : IFuncSettings<IPairwise, TResult>
        {
        }
    }
}