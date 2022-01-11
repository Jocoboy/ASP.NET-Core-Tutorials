using System.Collections.Generic;
using CSharp.Algorithms.Common; 

namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 冒泡排序
    /// </summary>
    public static class BubbleSorter
    {

        /// <summary>
        /// 升序
        /// </summary>
        public static void BubbleSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for(int i = 0; i < collection.Count; i++)
            {
                for(int j = 0; j < collection.Count - i - 1; j++)
                {
                    if(comparer.Compare(collection[j], collection[j + 1]) > 0)
                    {
                        collection.Swap(j, j + 1);
                    }
                }
            }
        }

        /// <summary>
        /// 降序
        /// </summary>
        public static void BubbleSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                for (int j = 1; j < collection.Count - i; j++)
                {
                    if (comparer.Compare(collection[j], collection[j - 1]) > 0)
                    {
                        collection.Swap(j-1, j);
                    }
                }
            }
        }

        /// <summary>
        /// 默认方法：升序
        /// </summary>
        public static void BubbleSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.BubbleSortAscending(comparer ?? Comparer<T>.Default);
        }
    }
}
