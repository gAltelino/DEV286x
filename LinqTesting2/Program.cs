using System;
using System.Linq;
using LinqTesting2.Models;

namespace LinqTesting2
{
    class Program
    {
        static void Main(string[] args)
        {
            //using the mysql world database as testing
            var dbContext = new WorldContext();
            var countries = dbContext.Country.ToList();

            foreach (var item in countries)
            {
                System.Console.WriteLine(item.Name + " " + item.Capital);
            }

        }
    }
}
