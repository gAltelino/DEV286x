using System;
using System.Collections.Generic;

namespace LinqTesting2.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
            Countrylanguage = new HashSet<Countrylanguage>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public float SurfaceArea { get; set; }
        public short? IndepYear { get; set; }
        public int Population { get; set; }
        public float? LifeExpectancy { get; set; }
        public float? Gnp { get; set; }
        public float? Gnpold { get; set; }
        public string LocalName { get; set; }
        public string GovernmentForm { get; set; }
        public string HeadOfState { get; set; }
        public int? Capital { get; set; }
        public string Code2 { get; set; }

        public ICollection<City> City { get; set; }
        public ICollection<Countrylanguage> Countrylanguage { get; set; }
    }
}
