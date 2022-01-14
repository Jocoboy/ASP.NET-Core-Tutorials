using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CSharp.Algorithms.Sorting;
using System.Linq;

namespace CSharp.UnitTest.Algorithms
{
    public static class InsertionSorterTest
    {
        [Fact]
        public static void DoTest()
        {
            var list = new List<int>() { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            list.InsertionSort();
            Assert.True(list.SequenceEqual(list.OrderBy(x => x)), "插入排序（升序）出错");

            var list2 = new List<char> { 'g', 'f', 'z', 'w', 'A', 'o', 'P' };
            list2.InsertionSort();
            char[] sortedList2 = { 'A', 'P', 'f', 'g', 'o', 'w', 'z' };
            Assert.True(list2.SequenceEqual(sortedList2), "插入排序（字符串升序）出错");
        }
    }
}
