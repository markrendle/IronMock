using IronMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IronMock.Test
{
    
    
    /// <summary>
    ///This is a test class for EnumerableExtensionsTest and is intended
    ///to contain all EnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnumerableExtensionsTest
    {
        public void ListEqualTestHelper<T>(IEnumerable<T> one, IEnumerable<T> two, bool expected)
        {
            var actual = one.ListEqual(two);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ListEqualIntPositiveTest()
        {
            ListEqualTestHelper(new[] {1,2,3,4}, new[] {1,2,3,4}, true);
        }

        [TestMethod()]
        public void ListEqualIntNegativeTest()
        {
            ListEqualTestHelper(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 5 }, false);
        }

        [TestMethod()]
        public void ListEqualIntNegativeShorterOneTest()
        {
            ListEqualTestHelper(new[] { 1 }, new[] { 1, 2 }, false);
        }

        [TestMethod()]
        public void ListEqualIntNegativeShorterTwoTest()
        {
            ListEqualTestHelper(new[] { 1, 2 }, new[] { 1 }, false);
        }

        [TestMethod()]
        public void ListEqualIntBothEmptyTest()
        {
            ListEqualTestHelper(Enumerable.Empty<int>(), Enumerable.Empty<int>(), true);
        }


        [TestMethod()]
        public void ListEqualIntOneEmptyTest()
        {
            ListEqualTestHelper(new[] { 1 }, Enumerable.Empty<int>(), false);
        }


        [TestMethod()]
        public void ListEqualIntTwoEmptyTest()
        {
            ListEqualTestHelper(Enumerable.Empty<int>(), new[] { 1 }, false);
        }
    }
}
