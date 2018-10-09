using System;

namespace ExtensionMethodExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = 3.14;
            var df = d.FormatWith("${}");
            System.Console.WriteLine(df);
        }
    }

    static class FormatExtension{
        public static string FormatWith(this object caller, string template){
        
        if (string.IsNullOrEmpty(template) || !template.Contains("{0}"))
        {
            throw new ArgumentException("Please provide a valid format template");
        }

        var result = string.Format(template, caller);
        return result;
        }
    }
}
