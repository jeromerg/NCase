using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using NCase.Back.Imp.Pairwise;
using NUnit.Framework;

namespace NCase.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class PairwiseAlgorithmTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            Generate(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCase(new int[]{})]
        [TestCase(new int[]{0})]
        [TestCase(new int[]{1})]
        [TestCase(new int[]{5})]
        [TestCase(new int[]{0, 0})]
        [TestCase(new int[]{5, 0})]
        [TestCase(new int[]{0, 5})]
        [TestCase(new int[]{1, 1, 1, 1, 0})]
        public void TestLessThanTwoDimsOrAtLeastOneEmptyDim(int[] dimSizes)
        {
            Generate(dimSizes);
        }

        [Test]
//        [TestCase(new int[]{1, 1})]
        [TestCase(new int[]{1, 1, 1})]
//        [TestCase(new int[]{1, 1, 1, 1, 1, 1, 1, 1})]
        public void TestSingleTuple(int[] dimSizes)
        {
            int[][] tuples = Generate(dimSizes);

            tuples.Should().HaveCount(1);
            tuples[0].Should().OnlyContain(v => v == 0);

        }


        private static int[][] Generate(int[] dimSizes)
        {
            return new PairwiseGenerator().Generate(dimSizes).ToArray();
        }
    }
}