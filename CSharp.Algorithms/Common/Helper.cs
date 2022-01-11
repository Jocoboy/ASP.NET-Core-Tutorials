using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Algorithms.Common
{
    public static class Helper
    {
        /// <summary>
        /// 交换IList<T>集合中给定索引的两个值
        /// </summary>
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex)
                return;

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
