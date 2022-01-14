using CSharp.Algorithms.Search;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharp.UnitTest.Algorithms
{
    public static class BinarySearcherTest
    {

        [Fact]
        public static void NullCollectionExceptionTest()
        {
            IList<int> list = null;
            Assert.Throws<System.NullReferenceException>(() => new BinarySearcher<int>(list, Comparer<int>.Default));
        }

        [Fact]
        public static void MoveNextTest()
        {
            IList<int> list = new List<int> { 3, 5, 2, 6, 1, 4 };
            BinarySearcher<int> intSearcher = new BinarySearcher<int>(list, Comparer<int>.Default);

            intSearcher.BinarySearch(1);
            intSearcher.Reset();
            IList<int> leftEnumeratedValues = new List<int> { 3, 2, 1 };
            int i = 0;
            while (intSearcher.MoveNext())
            {
                Assert.Equal(leftEnumeratedValues[i++], intSearcher.Current);
            }

            intSearcher.BinarySearch(6);
            intSearcher.Reset();
            IList<int> rightEnumeratedValues = new List<int> { 3, 5, 6 };
            int j = 0;
            while (intSearcher.MoveNext())
            {
                Assert.Equal(rightEnumeratedValues[j++], intSearcher.Current);
            }
        }

        [Fact]
        public static void IntBinarySearchTest()
        {
            IList<int> list = new List<int> { 9, 3, 7, 1, 6, 10 };
            IList<int> sortedList = new List<int> { 1, 3, 6, 7, 9, 10 };
            int numToSearch = 6;
            BinarySearcher<int> intSearcher = new BinarySearcher<int>(list, Comparer<int>.Default);
            int actualIndex = intSearcher.BinarySearch(numToSearch);
            int expectedIndex = sortedList.IndexOf(numToSearch);

            Assert.Equal(expectedIndex, actualIndex);
            Assert.Equal(numToSearch, intSearcher.Current);

            numToSearch = 20;
            int itemNotExist = intSearcher.BinarySearch(numToSearch);
            Assert.Equal(-1, itemNotExist);
        }

        [Fact]
        public static void StringBinarySearchTest()
        {
            IList<string> list = new List<string> { "lion", "cat", "tiger", "bee", "sparrow" };
            IList<string> sortedList = new List<string> { "bee", "cat", "lion", "sparrow", "tiger" };
            string stringToSearch = "bee";
            BinarySearcher<string> strSearcher = new BinarySearcher<string>(list, Comparer<string>.Default);
            int actualIndex = strSearcher.BinarySearch(stringToSearch);
            int expectedIndex = sortedList.IndexOf(stringToSearch);

            Assert.Equal(expectedIndex, actualIndex);
            Assert.Equal(stringToSearch, strSearcher.Current);

            stringToSearch = "shark";
            int itemNotExist = strSearcher.BinarySearch(stringToSearch);
            Assert.Equal(-1, itemNotExist);
        }
    }
}
