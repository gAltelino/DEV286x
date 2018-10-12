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
