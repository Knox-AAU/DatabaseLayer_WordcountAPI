using System;
using System.Collections;
using System.Collections.Generic;

namespace WordCount.Data.DataAccess
{
    public sealed class EventList<T> : IList<T>, IReadOnlyList<T>
    {
        public event Action<T> ItemAdded;
        public event Action<IEnumerable<T>> ItemsAdded;
        public event Action<int, T> ItemInserted;
        public event Action<T> ItemRemoved;
        public event Action ListCleared;

        public T this[int index]
        {
            get => internalList[index];
            set => internalList[index] = value;
        }

        public EventList(List<T> originalList)
        {
            internalList = originalList;
        }

        public bool IsReadOnly => false;

        int ICollection<T>.Count => internalList.Count;

        int IReadOnlyCollection<T>.Count => internalList.Count;
        public event Action<IEnumerable<T>> ItemsRemoved;

        private readonly List<T> internalList = new();

        public void Add(T item)
        {
            internalList.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void Clear()
        {
            internalList.Clear();
            ListCleared?.Invoke();
        }

        public bool Contains(T item) => internalList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => internalList.CopyTo(array, arrayIndex);

        bool ICollection<T>.Remove(T item)
        {
            if (!internalList.Remove(item))
            {
                return false;
            }

            ItemRemoved?.Invoke(item);
            return true;
        }

        public void Remove(T item)
        {
            internalList.Remove(item);
            ItemRemoved?.Invoke(item);
        }

        public IEnumerator<T> GetEnumerator() => internalList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();

        public int IndexOf(T item) => internalList.IndexOf(item);

        public void Insert(int index, T item)
        {
            internalList.Insert(index, item);
            ItemInserted?.Invoke(index, item);
        }

        public void RemoveAt(int index)
        {
            T item = this[index];

            internalList.RemoveAt(index);
            ItemRemoved?.Invoke(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            internalList.AddRange(items);
            ItemsAdded?.Invoke(items);
        }

        public void FindIndex()
        {
        }

        public int FindIndex(Predicate<T> func)
        {
            return internalList.FindIndex(func);
        }
    }
}