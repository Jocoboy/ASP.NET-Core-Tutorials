using System.Collections.Generic;
using CSharp.Algorithms.Common;

namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 快速排序（冒泡排序改进）
    /// </summary>
    public static class QuickSorter
    {
        private static int InternalPartition<T>(this IList<T> collection, int leftIndex, int rightIndex, Comparer<T> comparer)
        {
            //选择基准
            int pivotIndex = rightIndex;
            int wallIndex = leftIndex;

            for(int i = leftIndex; i <= rightIndex - 1 ; i++)
            {
                if(comparer.Compare(collection[i], collection[pivotIndex]) <= 0)
                {
                    collection.Swap(i, wallIndex);
                    wallIndex++;
                }
            }

            collection.Swap(wallIndex, pivotIndex);

            return wallIndex;
        }

        private static void InternalQuickSort<T>(this IList<T> collection, int leftIndex, int rightIndex, Comparer<T> comparer)
        {
            if(leftIndex < rightIndex)
            {
                int wallIndex = collection.InternalPartition(leftIndex, rightIndex, comparer);
                collection.InternalQuickSort(leftIndex, wallIndex - 1, comparer);
                collection.InternalQuickSort(wallIndex + 1, rightIndex, comparer);
            }
        }


        public static void QuickSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.InternalQuickSort(0, collection.Count - 1, comparer ?? Comparer<T>.Default);
        }
    }
}
