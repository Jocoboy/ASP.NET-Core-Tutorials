using System.Collections.Generic;
using System.Linq;
using Xunit;
using CSharp.Algorithms.Sorting;

namespace CSharp.UnitTest.Algorithms
{
    public static class QuickSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = new List<long>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            long[] sortedList = { 0, 2, 3, 4, 8, 9, 12, 15, 16, 23, 25, 34, 42, 46, 55 };
            list.QuickSort();
            Assert.True(list.SequenceEqual(sortedList));

            var list2 = new List<char>() { 'g', 'f', 'z', 'w', 'A', 'o', 'P' };
            char[] sortedList2 = { 'A', 'P', 'f', 'g', 'o', 'w', 'z' };
            list2.QuickSort();
            Assert.True(list2.SequenceEqual(sortedList2));
        }
    }
}
