using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LinqApp2;

namespace LinqApp2
{
    class Program
    {

        static void Main(string[] args)
        {
            var record = DataLoader.Load(@"/home/altrunox/Documents/DEV286x/LinqApp2");
            var female10 = record.Where(r => r.Gender == Gender.Female && r.Rank <= 10);
            var male10 = record.Where(m => m.Gender == Gender.Male && m.Rank <= 10);
            var maleAnotherWay = from r in record where r.Gender == Gender.Male && r.Rank <= 10 select r;

            foreach (var r in male10)
            {
                System.Console.WriteLine(r);
            }

            foreach (var r in female10)
            {
                System.Console.WriteLine(r);
            }

            foreach (var r in maleAnotherWay)
            {
                System.Console.WriteLine(r);
            }


        }
    }
}
