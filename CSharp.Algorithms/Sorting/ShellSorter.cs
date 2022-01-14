using CSharp.Algorithms.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 希尔排序（拆入排序改进）
    /// </summary>
    public static class ShellSorter
    {
        /// <summary>
        /// 升序
        /// </summary>
        public static void ShellSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            bool flag = true;
            int d = collection.Count;
            while(flag || d > 1)
            {
                flag = false;
                d = (d + 1) / 2;
                for(int  i = 0; i < collection.Count - d; i++)
                {
                    if(comparer.Compare(collection[i], collection[i + d]) >  0)
                    {
                        collection.Swap(i + d, i);
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// 默认方法：升序
        /// </summary>
        public static void ShellSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.ShellSortAscending(comparer ?? Comparer<T>.Default);
        }
    }
}
