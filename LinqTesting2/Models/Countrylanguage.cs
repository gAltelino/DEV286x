using System;
using System.Collections.Generic;

namespace LinqTesting2.Models
{
    public partial class Countrylanguage
    {
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public float Percentage { get; set; }

        public Country CountryCodeNavigation { get; set; }
    }
}
