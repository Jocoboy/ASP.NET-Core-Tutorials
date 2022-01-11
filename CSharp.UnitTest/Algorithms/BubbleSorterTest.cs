using System.Collections.Generic;
using System.Linq;
using Xunit;
using CSharp.Algorithms.Sorting;


namespace CSharp.UnitTest.Algorithms
{
    public static class BubbleSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = new List<int>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };

            list.BubbleSort();
            Assert.True(list.SequenceEqual(list.OrderBy(x => x)), "冒泡排序（升序）出错");

            //list.BubbleSortDescending(Comparer<int>.Default);
            //Assert.True(list.SequenceEqual(list.OrderByDescending(x => x)), "冒泡排序（降序）出错");

            var list2 = new List<char>() { 'g', 'f', 'z', 'w', 'A', 'o', 'P' };
            list2.BubbleSort();
            char[] sortedList2 = { 'A', 'P', 'f', 'g', 'o', 'w', 'z' };
            Assert.True(list2.SequenceEqual(sortedList2), "冒泡排序（字符串升序）出错");
        }           
    }
}
