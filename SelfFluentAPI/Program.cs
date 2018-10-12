using System;

namespace SelfFluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var writer = new Writer
            {
                Name = "Timothy",
                Scope = ".NET Core",
                YearsOfExperience = 15
            };

            writer.Introduce1().Introduce2().Introduce3();
        }
    }

    public interface IWorker
    {
        string Name { get; set; }
        int YearsOfExperience { get; set; }
        string Scope { get; set; }
    }

    public class Writer : IWorker
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string Scope { get; set; }
        public void Write() { /*...*/ }
    }

    public class Teacher : IWorker
    {
        public string Name { get; set; }
        public int YearsOfExperience { get; set; }
        public string Scope { get; set; }
        public void Teach() { /*...*/ }
    }

    public static class IWorkerExtension
    {
        public static IWorker Introduce1(this IWorker worker)
        {
            Console.WriteLine($"Hi, my name is {worker.Name}.");
            return worker;
        }

        public static IWorker Introduce2(this IWorker worker)
        {
            Console.WriteLine($"My major scope is {worker.Scope}.");
            return worker;
        }

        public static IWorker Introduce3(this IWorker worker)
        {
            Console.WriteLine($"I have {worker.YearsOfExperience} years experience.");
            return worker;
        }
    }
}