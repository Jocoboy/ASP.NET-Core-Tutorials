using System.Collections.Generic;


namespace CSharp.Algorithms.Sorting
{
    /// <summary>
    /// 插入排序
    /// </summary>
    public static class InsertionSorter
    {
        /// <summary>
        /// 升序
        /// </summary>
        public static void InsertionSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            for(int i = 1; i < collection.Count; i++)
            {
                T value = collection[i];
                int j = i - 1;
                while(j >= 0 && comparer.Compare(collection[j], value) > 0)
                {
                    collection[j + 1] = collection[j];
                    j--;
                }
                collection[j + 1] = value;
            }
        }

        /// <summary>
        /// 默认方法：升序
        /// </summary>
        public static void InsertionSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            collection.InsertionSortAscending(comparer ?? Comparer<T>.Default);
        }
    }
}
