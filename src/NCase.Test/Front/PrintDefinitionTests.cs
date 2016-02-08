using System;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test.Front
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class PrintDefinitionTests
    {
        [Test]
        public void ArgumentNullTest()
        {
            Assert.Throws<ArgumentNullException>( ()=>((SetDefBase<ISetDefModel<ISetDefId>, ISetDefId, Definer>)null).PrintDefinition());
        }
    }
}