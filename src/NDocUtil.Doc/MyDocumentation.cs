using System;
using NDocUtilLibrary;
using NUnit.Framework;

namespace NUtil.Doc
{
    [TestFixture]
    public class MyDocumentation
    {
        private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void PairwiseGenerator()
        {
            //# MY_CODE_SNIPPET
            var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");
            Console.WriteLine(someDate.ToString("o"));
            docu.StopRecordConsole();
            //#
        }
    }

}