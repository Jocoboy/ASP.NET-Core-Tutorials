using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Algorithms.Common;

namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 堆排序（选择排序改进）
    /// </summary>
    public static class HeapSorter
    {
        /// <summary>
        ///  在两个索引（包括）之间的元素建堆，在顶部保持最大值。
        /// </summary>
        private static void MaxHeapify<T>(this IList<T> collection, int nodeIndex, int lastIndex, Comparer<T> comparer)
        {
            //假设left和right是最大堆
            int left = (nodeIndex * 2) + 1;
            int right = left + 1;
            int largest = nodeIndex;

            if (left <= lastIndex && comparer.Compare(collection[left], collection[nodeIndex]) > 0)
                largest = left;

            if (right <= lastIndex && comparer.Compare(collection[right], collection[largest]) > 0)
                largest = right;

            //交换node和largest
            if(largest != nodeIndex)
            {
                collection.Swap(nodeIndex, largest);
                collection.MaxHeapify(largest, lastIndex, comparer);
            }
        }


        /// <summary>
        /// 构建一个最大堆
        /// </summary>
        private static void BuildMaxHeap<T>(this IList<T> collection, int firstIndex, int lastIndex, Comparer<T> comparer)
        {
            int lastNodeWithChildren = lastIndex / 2;

            for(int node = lastNodeWithChildren; node >= firstIndex; node--)
            {
                collection.MaxHeapify(node, lastIndex, comparer);
            }
        }


        /// <summary>
        /// 升序
        /// </summary>
        public static void HeapSortAscending<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            int lastIndex = collection.Count - 1;
            collection.BuildMaxHeap(0, lastIndex, comparer);

            while(lastIndex >= 0)
            {
                collection.Swap(0, lastIndex);
                lastIndex--;
                collection.MaxHeapify(0, lastIndex, comparer);
            }
        }

        /// <summary>
        /// 默认方法：升序
        /// 使用最大堆
        /// </summary>
        public static void HeapSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.HeapSortAscending(comparer ?? Comparer<T>.Default);
        }
    }
}
