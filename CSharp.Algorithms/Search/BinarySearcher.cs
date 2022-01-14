using CSharp.Algorithms.Sorting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Algorithms.Search
{
    public class BinarySearcher<T> : IEnumerable<T>
    {
        private readonly IList<T> _collection;
        private readonly Comparer<T> _comparer;
        private T _item;
        private int _currentItemIndex;
        private int _leftIndex;
        private int _rightIndex;

        /// <summary>
        /// 当前项的值
        /// </summary>
        public T Current
        {
            get
            {
                return _collection[_currentItemIndex];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Current;
        }

        public BinarySearcher(IList<T> collection, Comparer<T> comparer)
        {
            if (collection == null)
            {
                throw new NullReferenceException("List is null");
            }
            _collection = collection;
            _comparer = comparer;
            HeapSorter.HeapSort(_collection);
        }

        public void Reset()
        {
            _currentItemIndex = -1;
            _leftIndex = 0;
            _rightIndex = _collection.Count - 1;
        }

        /// <summary>
        /// 枚举器move-next方法的实现
        /// </summary>
        /// <returns>如果迭代可以继续到下一项，则为true，否则为false</returns>
        public bool MoveNext()
        {
            _currentItemIndex = _leftIndex + (_rightIndex - _leftIndex) / 2;

            if(_comparer.Compare(_item, Current) < 0)
            {
                _rightIndex = _currentItemIndex - 1;
            }
            else if(_comparer.Compare(_item, Current) > 0)
            {
                _leftIndex = _currentItemIndex + 1;
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  在List中二分查找
        /// </summary>
        /// <param name="item"></param>
        /// <returns>返回索引，未查找到则返回-1</returns>
        public int BinarySearch(T item)
        {
            bool notFound = true;
            Reset();
            _item = item;

            if (item == null)
            {
                throw new NullReferenceException("Item to search for is not set");
            }
            while (_leftIndex <= _rightIndex && notFound)
            {
                notFound = MoveNext();
            }

            if (notFound)
            {
                Reset();
            }
            return _currentItemIndex;
        }

    }
}
