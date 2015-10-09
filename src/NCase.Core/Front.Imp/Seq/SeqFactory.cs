using System;
using JetBrains.Annotations;
using NCase.Front.Api.Seq;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Seq
{
    public class SeqFactory : ISeqFactory
    {
        [NotNull] private readonly IServices<ISeqModel> mServices;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public SeqFactory([NotNull] IServices<ISeqModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (services == null) throw new ArgumentNullException("services");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mServices = services;
            mCodeLocationUtil = codeLocationUtil;
        }

        public ISeq Create(string defName, IBook book)
        {
            return new Seq(defName, book, mServices, mCodeLocationUtil);
        }
    }
}