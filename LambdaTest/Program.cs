using System;
using System.Linq;

namespace LambdaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> func = (s) => { return s.Length; };
            int x = func("Hello");
            Console.WriteLine(x);

            //simplified
            Func<string, int> funcS = s => s.Length;
            int y = funcS("Hello");
            Console.WriteLine(y);

            //another example
            Func<string, string> func1 = s => s.Substring(0, 3);
            Func<string, string> func2 = s => s;

            var r1 = func1("Timothy");
            var r2 = func2("Timothy");
            Console.WriteLine($"'{r1}' is the first three characters of '{r2}'.");

            //another with more parameters
            Func<string, int, string> strHead = (str, len) => str.Substring(0, len);
            var result1 = strHead("Timothy", 3);
            var result2 = strHead("Timothy", 1);
            System.Console.WriteLine(result1);
            System.Console.WriteLine(result2);

            //action has no return
            Action<double, double, double> sum3 = (x1, y1, z1) => Console.WriteLine(x1 + y1 + z1);

            //dont work below
            // var ActionResult = sum3(1.1, 2.2, 3.3);
            // Console.WriteLine(ActionResul);

            //no return, much works
            sum3(1.1, 2.2, 3.3);

        }
    }
}
