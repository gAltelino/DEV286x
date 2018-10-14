using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace LinqTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            //returning only one type
            var objects = new Object[] { 1, 10L, 1.1, 1.1f, "Hello", 2, 3 };
            int[] left = { 1, 1, 2, 3, 3, 4, 4 };
            int[] right = { 3, 4, 5, 6 };

            var result1 = objects.OfType<int>();

            foreach (var item in result1)
            {
                System.Console.WriteLine(item);
            }

            //check if all is less than 3
            var records = DataLoader.Load(@"/home/altrunox/Documents/DEV286x/LinqTesting");
            var result2 = records.All(r => r.Name.Length > 3);

            System.Console.WriteLine("All less than 3? : " + result2);

            //Challenge yourself: Can you find out what the short names are?
            var result3 = records.Where(r => r.Name.Length <= 3).Select(r => r.Name);
            System.Console.WriteLine("Those are smaller: ");
            foreach (var item in result3)
            {
                System.Console.WriteLine(item);
            }

            //Any operator - check names bigger than 15
            var result4 = records.Any(r => r.Name.Length > 15);
            System.Console.WriteLine("Are there names bigger than 15? :" + result4);

            if (result4 == true)
            {
                var resultIf1 = records.Where(r => r.Name.Length > 15).Select(r => r.Name);
                System.Console.WriteLine("Those are the names longer than 15: ");
                foreach (var item in resultIf1)
                {
                    System.Console.WriteLine(item);
                }
            }

            int[] integers = { 100, 200, 300, 400, 500 };
            string[] strings = { "Tim", "Tom", "Rina", "Andrew" };

            var result5 = integers.Contains(200);
            var result6 = strings.Contains("Tim");

            System.Console.WriteLine($"{result5} {result6}");

            var record = new Record("Ashley", Gender.Female, 1);

            var result7 = records.Contains(record, new RecordComparer());
            System.Console.WriteLine(result7);

            //testing set operations now
            var distinctResult = left.Distinct();
            var intersectResult = left.Intersect(right);
            var exceptResult = left.Except(right);
            var unionResult = left.Union(right);

            Console.WriteLine($"Distinct: {string.Join(",", distinctResult)}"); // 1, 2, 3, 4
            Console.WriteLine($"Intersect: {string.Join(",", intersectResult)}"); // 3, 4
            Console.WriteLine($"Except: {string.Join(",", exceptResult)}"); // 1, 2
            Console.WriteLine($"Union: {string.Join(",", unionResult)}"); // 1, 2, 3, 4, 5, 6

            var concatLeftRight = left.Concat(right);
            var unionLeftRight = left.Union(right);

            System.Console.WriteLine($"Concat: {string.Join(",", concatLeftRight)}");
            System.Console.WriteLine($"Union: {string.Join(",", unionLeftRight)}");

            //sorting operations
            var sortedByRank = records.OrderBy(r => r.Rank);
            foreach (var item in sortedByRank)
            {
                System.Console.WriteLine(item.ToString());
            }

            var sortedByRankDesc = records.OrderByDescending(r => r.Rank);
            foreach (var item in sortedByRankDesc)
            {
                System.Console.WriteLine(item.ToString());
            }

            var sortedThenBy = records.OrderBy(r => r.Rank).ThenBy(n => n.Name);

            foreach (var item in sortedThenBy)
            {
                System.Console.WriteLine(item.ToString());
            }

            //custom sorting
            var customSorted = records.OrderBy(r => r, new RecordComparer());

            foreach (var item in customSorted)
            {
                System.Console.WriteLine(item.ToString());
            }

            //select testing
            var selectedItens = records.Select(r => new RankAndName { Rank = r.Rank, Name = r.Name });
            foreach (var item in selectedItens)
            {
                System.Console.WriteLine($"Rank:{item.Rank} Name:{item.Name}");
            }

            //anonymous types
            var selectedItensAnon = records.Select(r => new { Rank = r.Rank, Name = r.Name });
            foreach (var item in selectedItensAnon)
            {
                System.Console.WriteLine($"Rank:{item.Rank} Name:{item.Name}");
            }

            //althought value tuples is probably a better idea
            var selectValueTuple = records.Select(r => (r.Rank, r.Name));
            foreach (var item in selectValueTuple)
            {
                System.Console.WriteLine($"Rank:{item.Rank} Name:{item.Name}");
            }

            //using select many, merges two or more collections (same type?) together
            var dict = new Dictionary<string, IEnumerable<Record>>();
            dict["FemaleTop5"] = records.Where(r => r.Rank <= 5 && r.Gender == Gender.Female);
            dict["MaleTop5"] = records.Where(r => r.Rank <= 5 && r.Gender == Gender.Male);

            var names2 = new List<string>();
            var selectManyResult = dict.SelectMany(kvp => kvp.Value);
            foreach (var r in selectManyResult)
            {
                names2.Add(r.Name);
            }

            foreach (var item in names2)
            {
                System.Console.WriteLine(item);
            }

            string[] names = { "Andrew", "Tim", "Tom", "Rina" };
            var eAt3 = names.ElementAt(3);
            var eAt4 = names.ElementAtOrDefault(4);
            var head = names.First();
            var firstT = names.First(n => n.StartsWith("T"));
            var firstX = names.FirstOrDefault(n => n.StartsWith("X"));
            var tail = names.Last();
            var lastT = names.Last(n => n.StartsWith("T"));
            var lastX = names.LastOrDefault(n => n.StartsWith("X"));
            //var onlyOne = names.SingleOrDefault(); need at least on parameter to work
            var singleA = names.Single(n => n.StartsWith("A"));
            //var singleT = names.SingleOrDefault(n => n.StartsWith("T"));
            var singleX = names.SingleOrDefault(n => n.StartsWith("X"));
            Console.WriteLine($"{eAt3}, {head}, {firstT}, {tail}, {lastT}, {singleA}");
            Console.WriteLine($"{eAt4}, {firstX}, {lastX}, {singleX}");

            //Partioning...
            string[] source = { "A1", "A2", "B1", "B2", "C1", "C2" };
            var r1 = source.Take(3);
            var r2 = source.Take(100);
            var r3 = source.Skip(2);
            var r4 = source.Skip(100);
            var r5 = source.Skip(2).Take(2);
            var r6 = source.TakeLast(2);
            var r7 = source.TakeLast(100);

            Console.WriteLine(String.Join(",", r1));
            Console.WriteLine(String.Join(",", r2));
            Console.WriteLine(String.Join(",", r3));
            Console.WriteLine(String.Join(",", r4));
            Console.WriteLine(String.Join(",", r5));
            Console.WriteLine(String.Join(",", r6));
            Console.WriteLine(String.Join(",", r7));

            //Partitioning using comparators, should use a linq to check if it's still true
            var r1L = source.TakeWhile(e => e.StartsWith("A"));
            var r2L = source.TakeWhile(e => !e.StartsWith("C"));
            var r3L = source.TakeWhile(e => e.StartsWith("C"));
            var r4L = source.SkipWhile(e => e.StartsWith("A"));
            var r5L = source.SkipWhile(e => !e.StartsWith("C"));
            var r6L = source.SkipWhile(e => e.StartsWith("C"));

            Console.WriteLine(String.Join(",", r1L));
            Console.WriteLine(String.Join(",", r2L));
            Console.WriteLine(String.Join(",", r3L));
            Console.WriteLine(String.Join(",", r4L));
            Console.WriteLine(String.Join(",", r5L));
            Console.WriteLine(String.Join(",", r6L));

            //USING repaat and range operators
            var r1R = Enumerable.Repeat("Hello", 5); //add hello 5 times
            var r2R = Enumerable.Range(0, 10); //range from 0 to 9, 10 itens added
            var r3R = Enumerable.Range(0, 10).Select(e => Math.Pow(2, e));// range of 9 itens, doig the select operations to create new itens
            var r4R = Enumerable.Range('A', 26).Select(e => (char)e); //alphabet

            Console.WriteLine(String.Join(",", r1R));
            Console.WriteLine(String.Join(",", r2R));
            Console.WriteLine(String.Join(",", r3R));
            Console.WriteLine(String.Join(",", r4R));

            var femaleTop100 = records.Where(r => r.Rank <= 100 && r.Gender == Gender.Female);

            var listF = femaleTop100.ToList();
            var arrayF = femaleTop100.ToArray();
            var setF = femaleTop100.ToHashSet();
            var dictF = femaleTop100.ToDictionary(r => r.Name, r => r.Rank);
            var lookupF = femaleTop100.ToLookup(r => (r.Rank - 1) / 10, r => r.Name);

            // Check collection type
            System.Console.WriteLine(femaleTop100.GetType());
            System.Console.WriteLine(listF.GetType());
            System.Console.WriteLine(arrayF.GetType());
            System.Console.WriteLine(setF.GetType());
            System.Console.WriteLine(dictF.GetType());
            System.Console.WriteLine(lookupF.GetType());
            System.Console.WriteLine(lookupF.First().GetType());

            System.Console.WriteLine("=======================");

            System.Console.WriteLine(dictF["Amy"]);
            System.Console.WriteLine(String.Join(",", lookupF[0]));

            //using SequenceEqual
            var array = new int[] { 0, 1, 2, 3, 4, 5 };
            var list = new List<int> { 0, 1, 2, 3, 4, 5 };
            var set = new HashSet<int> { 0, 1, 2, 3, 3, 2, 1, 4, 5 };
            var r1S = array.SequenceEqual(list);
            var r2S = array.SequenceEqual(set);
            var r3S = set.SequenceEqual(array);
            System.Console.WriteLine(r1S);
            System.Console.WriteLine(r2S);
            System.Console.WriteLine(r3S);

            string[] lower = { "aaa", "bbb", "ccc" };
            string[] upper = { "AAA", "BBB", "CCC" };

            var r1C = lower.SequenceEqual(upper);
            var r2C = lower.SequenceEqual(upper, new StringEqualityComparer());

            System.Console.WriteLine(r1C);
            System.Console.WriteLine(r2C);

            //Agregations
            double[] sourceA = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            var max = sourceA.Max();
            var min = sourceA.Min();
            var sum = sourceA.Sum();
            var count = sourceA.Count();
            var longCount = sourceA.LongCount();
            var avg = sourceA.Average();

            var resultA = sourceA.Aggregate((variance: 0.0, avg: sourceA.Average(), count: sourceA.Count()), (acc, e) =>
            {
                acc.variance += Math.Pow(e - acc.avg, 2) / acc.count;
                return acc;
            });

            Console.WriteLine($"{max.GetType().Name} {max}");
            Console.WriteLine($"{min.GetType().Name} {min}");
            Console.WriteLine($"{sum.GetType().Name} {sum}");
            Console.WriteLine($"{count.GetType().Name} {count}");
            Console.WriteLine($"{longCount.GetType().Name} {longCount}");
            Console.WriteLine($"{avg.GetType().Name} {avg}");
            Console.WriteLine($"{resultA.variance.GetType().Name} {resultA.variance}");

            var group1 = femaleTop100.GroupBy(r => r.Name[0]);
            var group2 = from r in femaleTop100 group r by r.Name[0];

            var group3 = femaleTop100.GroupBy(r => r.Name[0], r => r.Name);
            var group4 = from r in femaleTop100 group r.Name by r.Name[0];

            foreach (var g in group3)
            {
                Console.WriteLine($"Key:{g.Key} Count:{g.Count()} Names:{String.Join(",", g)}");

            }

            var resultGBS = femaleTop100.GroupBy(r => r.Name[0], r => r.Name).Select(g => (Key: g.Key, Count: g.Count(), Names: String.Join(",", g)));

            foreach (var e in resultGBS)
            {
                Console.WriteLine($"Key:{e.Key} Count:{e.Count} Names:{e.Names}");
            }

            var femaleTop51 = records
                .Where(r => r.Gender == Gender.Female && r.Rank <= 5);

            var femaleTop52 = records
.Where(r => r.Gender == Gender.Female && r.Rank <= 5);

var resultJoin = Enumerable.Join(
    femaleTop51, femaleTop52,
    r => r.Rank, r => r.Rank, (mr, fr) => (Rank: mr.Rank, Fem1: mr.Name, Fem2: fr.Name)
);

            System.Console.WriteLine($"Rank\tMale\tFemale");
            foreach (var item in resultJoin) {
                System.Console.WriteLine($"{item.Rank}\t{item.Fem1}\t{item.Fem2}");
            }

            var resultGroupJoinTest = femaleTop100.ToLookup(r => r.Name[0], r => r.Name).OrderBy(g => g.Key);

            foreach (var item in resultGroupJoinTest) {
                Console.WriteLine($"{item.Key}: {string.Join(",", item)}");
            }

            //groupjoin use case
            var alphabet = Enumerable.Range('A', 26).Select(e => (char)e);

            var resultGroupJoin = Enumerable.GroupJoin(
                alphabet, femaleTop100, 
                c => c, r => r.Name[0],
                (c, g) => (Start:c, Names: String.Join(",", g.Select(r => r.Name)))
            );

            foreach (var item in resultGroupJoin) {
                Console.WriteLine($"{item.Start}: {item.Names}");
            }

        }

        class StringEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return String.Compare(x, y, true) == 0;
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        class RankAndName
        {
            public int Rank { get; set; }
            public string Name { get; set; }
        }

        class CustomSort : IComparer<Record>
        {
            public int Compare(Record x, Record y)
            {
                if (x.Rank < y.Rank)
                {
                    return -1;
                }
                else if (x.Rank > y.Rank)
                {
                    return 1;
                }

                if (x.Gender < y.Gender)
                {
                    return -1;
                }
                else if (x.Gender > y.Gender)
                {
                    return 1;
                }

                return String.Compare(x.Name, y.Name);
            }
        }

        class RecordComparer : IEqualityComparer<Record>, IComparer<Record>
        {
            public bool Equals(Record x, Record y)
            {
                return x.Name == y.Name && x.Gender == y.Gender && x.Rank == y.Rank;
            }

            public int GetHashCode(Record obj)
            {
                return obj.GetHashCode();
            }

            public int Compare(Record x, Record y)
            {
                if (x.Rank < y.Rank)
                {
                    return -1;
                }
                else if (x.Rank > y.Rank)
                {
                    return 1;
                }

                if (x.Gender < y.Gender)
                {
                    return -1;
                }
                else if (x.Gender > y.Gender)
                {
                    return 1;
                }

                return String.Compare(x.Name, y.Name);
            }
        }
    }
}
