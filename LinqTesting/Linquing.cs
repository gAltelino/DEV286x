using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace LinqTesting
{
    public enum Gender
    {
        Female,
        Male
    }

    public class Record
    {
        public string Name { get; }
        public Gender Gender { get; }
        public int Rank { get; set; }

        public Record(string name, Gender gender, int rank)
        {
            Name = name;
            Gender = gender;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Gender}\t{Rank}\t{Name}";
        }
    }
    public class DataLoader
    {
        private static IEnumerable<Record> Load(string file, Gender gender)
        {
            var reader = new StreamReader(File.OpenRead(file));
            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine().Split(",");
                yield return new Record(values[1], gender, int.Parse(values[0]));
            }
        }

        public static IList<Record> Load(string folder)
        {
            var records = new List<Record>();
            records.AddRange(Load(Path.Combine(folder, "female.csv"), Gender.Female));
            return records;
        }
    }
}
