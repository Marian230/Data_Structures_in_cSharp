using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests
{
    public class SinglyLinkedListTests
    {
        public SinglyLinkedList<int> list { get; set; }

        [Fact]
        public void AddFirstItemToLinkedListTest()
        {
            list = new SinglyLinkedList<int>();
            list.AddLast(1);
            Assert.Equal(1, list.First.Value);
            Assert.Equal(1, list.Last.Value);

            list = new SinglyLinkedList<int>();
            list.AddFirst(1);
            Assert.Equal(1, list.First.Value);
            Assert.Equal(1, list.Last.Value);
        }

        [Fact]
        public void AddMultipleItemsToLinkedListTest()
        {
            list = new SinglyLinkedList<int>();
            for (int i = 0; i < 10; i++)
                list.AddFirst(i);

            for (int i = 9; i >= 0; i--)
                Assert.Equal(i, list.Find(Math.Abs(i - 9)).Value);
            Assert.Equal(10, list.Count);



            list = new SinglyLinkedList<int>();
            for (int i = 0; i < 10; i++)
                list.AddLast(i);

            for (int i = 0; i < 10; i++)
                Assert.Equal(i, list.Find(i).Value);
            Assert.Equal(10, list.Count);



            list.AddBefore(list.Find(list.Count / 2), 666);
            list.AddBefore(list.Find(list.Count / 2), 333);
            Assert.Equal(4, list.Find(4).Value);
            Assert.Equal(333, list.Find(5).Value);
            Assert.Equal(666, list.Find(6).Value);
            Assert.Equal(5, list.Find(7).Value);



            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            list.AddAfter(list.Find(list.Count / 2), 666);
            list.AddAfter(list.Find(list.Count / 2), 333);
            Assert.Equal(5, list.Find(5).Value);
            Assert.Equal(333, list.Find(6).Value);
            Assert.Equal(666, list.Find(7).Value);
            Assert.Equal(6, list.Find(8).Value);
        }

        [Fact]
        public void RemoveAtFromLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            SinglyNode<int> node = new SinglyNode<int>(666);

            list.AddBefore(list.Find(list.Count / 2), node);

            list.Remove(node);
            Assert.Equal(5, list.Find(5).Value);

            list.Remove(9);
            list.Remove(8);
            Assert.Equal(7, list.Find(list.Count - 1).Value);
            Assert.Equal(8, list.Count);
            Assert.Equal(list.Find(list.Count - 1), list.Last);
        }

        [Fact]
        public void RemoveFromLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            list.RemoveFirst();
            list.RemoveFirst();

            Assert.Equal(2, list.Find(0).Value);
            Assert.Equal(8, list.Count);


            list.RemoveLast();
            list.RemoveLast();

            Assert.Equal(7, list.Last.Value);
            Assert.Equal(7, list.Find(5).Value);
            Assert.Equal(6, list.Count);
            Assert.Equal(list.Find(0), list.First);
        }

        [Fact]
        public void GetIndexByNodeLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            var node = new SinglyNode<int>(666);
            int nodeIndex = 8;


            list.AddBefore(list.Find(nodeIndex), node);

            Assert.Equal(nodeIndex, list.GetIndexByNode(node));
        }

        [Fact]
        public void FindLambdaLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            var node = new SinglyNode<int>(666);
            int nodeIndex = 8;

            list.AddAfter(list.Find(nodeIndex), node);

            var newNode = list.Find(x => SinglyLinkedList<int>.Compare(x.Next.Value, node.Value));

            Assert.Equal(list.Find(nodeIndex), newNode);
        }

        [Fact]
        public void ContainsLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            var node = new SinglyNode<int>(666);

            list.AddAfter(list.Find(5), node);

            Assert.True(list.Contains(6));
            Assert.True(list.Contains(node));

            Assert.False(list.Contains(23));
            Assert.False(list.Contains(new SinglyNode<int>(23)));
        }

        [Fact]
        public void WhereLambdaLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            var list2 = new SinglyLinkedList<int>();

            var listWhere = list.Where(x => x % 2 == 0);

            for (int i = 0; i < list.Count; i++)
            {
                int? item = list.Find(i).Value;
                if (item == null)
                    continue;

                if (item % 2 == 0)
                    list2.AddLast((int)item);
            }

            for (int i = 0; i < list2.Count; i++)
                Assert.Equal(list2.Find(i).Value, listWhere[i]);
        }

        [Fact]
        public void ForEachLoopLinkedListTest()
        {
            list = new SinglyLinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            var list2 = new SinglyLinkedList<int>();

            foreach (var item in list)
                list2.AddLast(item);

            for (int i = 0; i < list.Count; i++)
                Assert.Equal(list.Find(i).Value, list2.Find(i).Value); 
        }

        [Fact]
        public void OutOfRangeFindByIndexLinkedList()
        {
            list = new SinglyLinkedList<int>();

            Assert.ThrowsAny<IndexOutOfRangeException>(() => list.Find(-1));
            Assert.ThrowsAny<IndexOutOfRangeException>(() => list.Find(0));


            list = new SinglyLinkedList<int>(0, 1);

            Assert.ThrowsAny<IndexOutOfRangeException>(() => list.Find(2));
        }
    }
}
