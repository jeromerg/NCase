﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        [TestCase(new []{1, 1})]
        [TestCase(new []{1, 1, 1})]
        [TestCase(new []{1, 1, 1, 1, 1, 1, 1, 1})]
        public void TestSingleTuple(int[] dimSizes)
        {
            int[][] tuples = Generate(dimSizes);

            Assert.AreEqual(1, tuples.Length);
            Assert.AreEqual(Enumerable.Repeat(0, dimSizes.Length), tuples[0]);
        }

        [Test]
        public void TestTwoTuples()
        {
            int[] dimSizes = {1, 2};

            int[][] tuples = Generate(dimSizes);

            Assert.AreEqual(2, tuples.Length);
            Assert.Contains(new[] { 0, 0 }, tuples);
            Assert.Contains(new[] { 0, 1 }, tuples);
        }

        [Test]
        [TestCase()]
        public void TestTwoTuples2()
        {
            int[] dimSizes = {1, 2, 1, 1, 1};

            int[][] tuples = Generate(dimSizes);

            Assert.AreEqual(2, tuples.Length);
            Assert.Contains(new[] { 0, 0, 0, 0, 0 }, tuples);
            Assert.Contains(new[] { 0, 1, 0, 0, 0 }, tuples);
        }


        [Test]
        [TestCase()]
        public void TestThreeTuples()
        {
            int[] dimSizes = {1, 3, 1, 1, 1};
            int[][] tuples = Generate(dimSizes);

            Assert.AreEqual(3, tuples.Length);
            Assert.Contains(new[] { 0, 0, 0, 0, 0 }, tuples);
            Assert.Contains(new[] { 0, 1, 0, 0, 0 }, tuples);
            Assert.Contains(new[] { 0, 2, 0, 0, 0 }, tuples);
        }


        [Test]
        public void TestFourTuples()
        {
            int[] dimSizes = {2, 2, 1, 1};
            
            int[][] tuples = Generate(dimSizes);

            Assert.AreEqual(4, tuples.Length);
            Assert.Contains(new[] { 0, 0, 0, 0 }, tuples);
            Assert.Contains(new[] { 1, 1, 0, 0 }, tuples);
            Assert.Contains(new[] { 0, 1, 0, 0 }, tuples);
            Assert.Contains(new[] { 1, 0, 0, 0 }, tuples);
        }

        [Test]
        public void TestManyTuples()
        {
            int[] dimSizes = {15, 10, 3, 1};

            int[][] tuples = Generate(dimSizes);

            // check that all pairs are covered
            for(int dim1=0; dim1<3; dim1++)
                for (int dim2 = dim1 + 1; dim2 < 4; dim2++)
                    for (int val1 = 0; val1 < dimSizes[dim1]; val1++)
                        for (int val2 = 0; val2 < dimSizes[dim2]; val2++)
                        {
                            int count = tuples.Count(t => t[dim1] == val1 && t[dim2] == val2);
                            Assert.Greater(0, count);
                        }
        }

        private static int[][] Generate(int[] dimSizes)
        {
            return new PairwiseGenerator().Generate(dimSizes).ToArray();
        }
    }
}