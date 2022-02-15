using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class MyList<T> : IEnumerable
    {
        private T[] array;
        private int defaultSize = 2;

        public int Count { get; set; }

        public MyList()
        {
            this.array = new T[this.defaultSize];
        }

        public MyList(params T[] array)
        {
            this.array = array;
            this.Count = array.Length;
        }
        public MyList(int size) 
        {
            this.array = new T[size];
        }

        public T Get(int index)
        {
            this.CheckRange(index);

            return this.array[index];
        }

        public void Set(int index, T value)
        {
            this.CheckRange(index);

            this.array[index] = value;
        }

        public T this[int i]
        {
            get { return this.Get(i); }
            set { this.Set(i, value); }
        }

        public void Add(T item)
        {
            this.CheckRange();

            this.array[this.Count++] = item;
        }

        public void AddRange(params T[] values)
        {
            if (values == null)
                return;
            if (values.Length == 0)
                return;

            T[] newArray = new T[this.Count + values.Length];

            for (int i = 0; i < this.Count + values.Length; i++)
                if (i < this.Count)
                    newArray[i] = this.array[i];
                else
                    newArray[i] = values[i - this.Count];

            this.array = newArray;
            this.Count += values.Length;
        }

        public void RemoveAt(int index)
        {
            this.CheckRange(index);

            for (int i = index; i < this.Count - 1; i++)
                this.array[i] = this.array[i + 1];

            this.Count--;
        }

        public bool Remove(T value)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(this.array[i], value))
                {
                    this.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.array = new T[this.defaultSize];
        }

        public void ForEach(Action<T> action)
        {
            for (int i = 0; i < this.Count; i++)
                action(this.array[i]);
        }

        public MyList<T> Where(Predicate<T> predicate)
        {
            MyList<T> renturnList = new MyList<T>();
            for (int i = 0; i < this.Count; i++)
            {
                if (predicate(this.array[i]))
                    renturnList.Add(this.array[i]);
            }

            return renturnList;
        }

        private void CheckRange()
        {
            if (this.Count >= this.array.Length)
                this.ExtendArray();
        }

        private void CheckRange(int index)
        {
            if (index >= this.Count)
                throw new ArgumentOutOfRangeException("accesing item out of lists range");
        }

        private void ExtendArray()
        {
            T[] newArray = new T[this.array.Length * 2];
            for (int i = 0; i < this.array.Length; i++)
                newArray[i] = this.array[i];

            this.array = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }
    }
}
