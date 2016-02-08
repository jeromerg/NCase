using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using NCaseFramework.Front.Ui;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test.Front
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class NewCombinationSetTests
    {

        [Test]
        public void FirstArgumentNullTest()
        {
            Assert.Throws<ArgumentNullException>( ()=>((CaseBuilder)null).NewCombinationSet("aName"));
        }

        [Test]
        public void SecondArgumentNullTest()
        {
            NCase.NewBuilder();
            CaseBuilder caseBuilder = new Mock<CaseBuilder>().Object;

            Assert.Throws<ArgumentNullException>( ()=> caseBuilder.NewCombinationSet(null));
        }

    }
}