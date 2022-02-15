using DataStructures;

MyList<int> list = new MyList<int>();
List<int> ml  = new List<int>();
list.Add(555);
list.Add(666);
list.AddRange(1, 2, 3, 4, 5, 6, 7, 8);
int[] pepa = new int[] { 45, 46, 47 };
list.AddRange(pepa);

list.ForEach(x => Console.Write(x + ", "));
Console.WriteLine();

list.Remove(45);

list.ForEach(x => Console.Write(x + ", "));
Console.WriteLine();

list.Where(x => x % 2 == 0)
    .ForEach(x => Console.Write(x + ", "));

Console.ReadKey();