using System.Collections.Generic;
using CSharp.Algorithms.Common;

namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 选择排序
    /// </summary>
    public static class SelectionSorter
    {
        /// <summary>
        /// 升序
        /// </summary>
        public static void SelectionSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for(int i = 0; i < collection.Count; i++)
            {
                int minIndex = i;
                for(int j = i + 1; j < collection.Count; j++)
                {
                    if(comparer.Compare(collection[j], collection[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }
                collection.Swap(i, minIndex);
            }
        }

        /// <summary>
        /// 降序
        /// </summary>
        public static void SelectionSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for(int i = 0; i < collection.Count; i++)
            {
                int maxIndex = i;
                for(int j = i + 1; j < collection.Count; j++)
                {
                    if(comparer.Compare(collection[j],collection[maxIndex]) > 0)
                    {
                        maxIndex = j;
                    }
                }
                collection.Swap(i, maxIndex);
            }
        }

        /// <summary>
        /// 默认方法：升序
        /// </summary>
        public static void SelectionSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.SelectionSortAscending(comparer ?? Comparer<T>.Default);
        }
    }
}
