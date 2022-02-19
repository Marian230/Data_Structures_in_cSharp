using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public SinglyNode<T> First { get; set; }

        public SinglyNode<T> Last { get; set; }

        public int Count { get; private set; }

        public SinglyLinkedList()
        {
        }

        public SinglyLinkedList(params T[] array)
        {
            foreach (var item in array)
                this.AddLast(item);
        }

        public void AddFirst(T value)
        {
            this.AddFirst(new SinglyNode<T>(value));
        }

        public void AddFirst(SinglyNode<T> node)
        {
            if (this.First == null)
            {
                this.First = node;
                this.Last = node;
            }

            node.Next = this.First;
            this.First = node;

            this.Count++;
        }

        public void AddLast(T value)
        {
            this.AddLast(new SinglyNode<T>(value));
        }

        public void AddLast(SinglyNode<T> node)
        {
            if (this.First == null)
                this.First = node;
            else if (this.Last == null)
                this.First.Next = node;
            else
                this.Last.Next = node;

            this.Last = node;
            this.Count++;
        }

        public void AddAfter(SinglyNode<T> after, T newItem)
        {
            this.AddAfter(after, new SinglyNode<T>(newItem));
        }

        public void AddAfter(SinglyNode<T> after, SinglyNode<T> newNode)
        {
            if (!this.Contains(after))
                throw new ArgumentOutOfRangeException("The LinkedList node does not belong to current LinkedList");

            SinglyNode<T> tmpNode = after.Next;
            after.Next = newNode;
            newNode.Next = tmpNode;

            this.Count++;
        }

        public void AddBefore(SinglyNode<T> before, T newItem)
        {
            this.AddBefore(before, new SinglyNode<T>(newItem));
        }

        public void AddBefore(SinglyNode<T> before, SinglyNode<T> newNode)
        {
            if (!this.Contains(before))
                throw new ArgumentOutOfRangeException("The LinkedList node does not belong to current LinkedList");

            if (this.First == before)
            {
                this.AddFirst(newNode);
                return;
            }

            for (SinglyNode<T> currNode = this.First.Next; currNode.Next != null; currNode = currNode.Next)
            {
                if (before == currNode.Next)
                {
                    this.AddAfter(currNode, newNode);
                    return;
                }
            }
        }

        public void Remove(SinglyNode<T> node)
        {
            if      (node == null)
                return;

            if      (node == this.First)
            {
                this.RemoveFirst();
                return;
            }

            var tmpNode = this.Find(x => x.Next == node);
            tmpNode.Next = node.Next;

            if (this.Last == node)
                this.Last = tmpNode;

            this.Count--;
        }

        public void Remove(T value)
        {
            if (value == null)
                return;

            if (Compare(this.First.Value, value))
                this.RemoveFirst();
            else
                this.InternalRemove(this.Find(x => Compare(x.Next.Value, value)));
        }

        // helping method for removing
        private void InternalRemove(SinglyNode<T> beforeRemoved)
        {
            if (beforeRemoved.Next == this.Last)
                this.Last = beforeRemoved;
            beforeRemoved.Next = beforeRemoved.Next.Next;

            this.Count--;
        }

        public void RemoveFirst()
        {
            if (this.First == null)
                throw new IndexOutOfRangeException("LinkedList is empty");

            this.First = this.First.Next;
            this.Count--;
        }

        public void RemoveLast()
        {
            if (this.Last == null)
                throw new IndexOutOfRangeException("LinkedList is empty");

            this.Remove(this.Last);
        }

        public void Clear()
        {
            this.First = this.Last = null;
            this.Count = 0;
        }

        public int? GetIndexByNode(SinglyNode<T> searchedNode)
        {
            SinglyNode<T> currNode = this.First;
            for (int i = 0; i < this.Count; i++)
            {
                if (currNode == searchedNode)
                    return i;
                currNode = currNode.Next;
            }

            return null;
        }

        public SinglyNode<T> Find(int index)
        {
            if (index >= this.Count && index < 0)
                throw new IndexOutOfRangeException("trying access item out of LinkedList");

            SinglyNode<T> currNode = this.First;
            for (int i = 0; i < index; i++)
                currNode = currNode.Next;

            return currNode;
        }

        public SinglyNode<T> Find(Predicate<SinglyNode<T>> predicate)
        {
            for (SinglyNode<T> currNode = this.First; currNode != null; currNode = currNode.Next)
                if (predicate(currNode))
                    return currNode;

            return null;
        }

        public bool Contains(T value)
        {
            if (value == null)
                return false;

            foreach (var item in this)
                if (Compare(item, value))
                    return true;

            return false;
        }

        public bool Contains(SinglyNode<T> node)
        {
            if (node == null)
                return false;

            for (SinglyNode<T> currNode = this.First; currNode != null; currNode = currNode.Next)
                if (currNode == node)
                    return true;

            return false;
        }

        public void ForEach(Action<T> action)
        {
            foreach (var item in this)
                action(item);
        }

        public MyList<T> Where(Predicate<T> predicate)
        {
            MyList<T> returnList = new MyList<T>();

            foreach (var item in this)
                if (predicate(item))
                    returnList.Add(item);

            return returnList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (SinglyNode<T> currNode = this.First; currNode != null; currNode = currNode.Next)
                yield return currNode.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }

    public class SinglyNode<T>
    {
        public SinglyNode(T value)
        {
            this.Value = value;
        }

        public SinglyNode(T value, SinglyNode<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        public T Value { get; set; }

        public SinglyNode<T> Next { get; set; }
    }
}