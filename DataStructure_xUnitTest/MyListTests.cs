using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using Xunit;

namespace DataStructures.Tests
{
    public class MyListTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1000, 1)]
        [InlineData(99999, 3, 2, 0, -33, 99)]
        public void AddingToEmptyListTest(params int[] values)
        {
            MyList<int> list = new MyList<int>();

            
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(i, list.Count);
                list.Add(values[i]);
            }

            
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], list.Get(i));
                Assert.Equal(values[i], list[i]);
            }
        }

        [Fact]
        public void AddRangeToList()
        {
            MyList<int> list = new MyList<int>();

            int[] values = new int[] { 1, 2, 3, -15, 158 };
            list.AddRange(values);

            Assert.Equal(values.Length, list.Count);

            for (int i = 0; i < values.Length; i++)
                Assert.Equal(values[i], list[i]);
        }

        [Fact]
        public void OutOfRangeAccesListTest()
        {
            MyList<int> list = new MyList<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => list[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[0] = 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Set(0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => { var item = list[0]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var item = list.Get(0); });

            Assert.Throws<ArgumentOutOfRangeException>(() => list[-2]);
            Assert.Throws<ArgumentOutOfRangeException>(() => { var item = list[-2]; });

            list.AddRange(3, 4);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[2]);
        }

        [Fact]
        public void RemoveItemAtIndexFromListTest()
        {
            MyList<int> list;
            int[] values = new int[] { 1, 2, 3, -15, 158 };
            

            for (int i = 0; i < values.Length; i++)
            {
                list = new MyList<int>(values);

                var newArr = values.Where(x => x != list[i]).ToArray();
                list.RemoveAt(i);

                for (int j = 0; j < list.Count; j++)
                {
                    Assert.Equal(newArr[j], list[j]);
                }
            }
        }

        [Fact]
        public void RemoveItemFromListTest()
        {
            MyList<int> list;
            int[] values = new int[] { 1, 2, 3, -15, 158 };

            for (int i = 0; i < values.Length; i++)
            {
                list = new MyList<int>(values);

                int value = values[i];
                list.Remove(values[i]);
                var newArr = values.Where(x => x != value);

                for (int j = 0; j < list.Count; j++)
                {
                    Assert.Equal(list[j], list[j]);
                }
            }
        }

        [Fact]
        public void ForEachLambdaListTest()
        {
            MyList<int> list = new MyList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            MyList<int> list2 = new MyList<int>();
            MyList<int> list3 = new MyList<int>();

            for (int i = 0; i < list.Count; i++)
                if (list[i] % 2 == 0)
                    list2.Add(list[i]);
            
            list.ForEach(x => {
                if (x % 2 == 0)
                    list3.Add(x);
            });


            Assert.Equal(list2.Count, list3.Count);
            for (int i = 0; i < list2.Count; i++)
                Assert.Equal(list2[i], list3[i]);
        }

        [Fact]
        public void WhereLambdaListTest()
        {
            MyList<int> preList = new MyList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            MyList<int> list = new MyList<int>();
            MyList<int> list2;

            for (int i = 0; i < preList.Count; i++)
                if (preList[i] % 2 == 0)
                    list.Add(preList[i]);

            list2 = preList.Where(x => x % 2 == 0);

            for (int i = 0; i < list.Count; i++)
                Assert.Equal(list[i], list2[i]);
        }

        [Fact]
        public void ForeachLoopListTest()
        {
            MyList<int> list = new MyList<int>(1, 2, 3, -15, 158);
            MyList<int> foreachList = new MyList<int>();

            foreach (var item in list)
                foreachList.Add(item);

            for (int i = 0; i < list.Count; i++)
                Assert.Equal(list[i], foreachList[i]);
        }

        [Fact]
        public void GenericsListTest()
        {
            GenericsListTestHelper(666, 889, 1, 2, 3, -15, 158, 0);
            GenericsListTestHelper("Foo", "Moo", "al", "be", "Hexakosioihexekontahexaphobia", "nothing");
        }

        public void GenericsListTestHelper<T>(T addItem, T removeItem, params T[] values)
        {
            MyList<T> list = new MyList<T>();

            list.Add(addItem);
            Assert.Equal(addItem, list[0]);

            list.Add(removeItem);

            list.AddRange(values);
            for (int i = 0; i < values.Length; i++)
                Assert.Equal(values[i], list[i + 2]);

            list[3] = removeItem;
            list.Remove(removeItem);

            Assert.Equal(values[0], list[1]);
        }
    }
}