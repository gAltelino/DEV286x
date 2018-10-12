using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionDelegateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Student() { ID = 0, Name = "Test" };
            var students = new List<Student>(){
                new Student{ID = 1, Name="Tim"},
                new Student{ID = 2, Name="Ella"},
                new Student{ID = 3, Name="Tom"},
                new Student{ID = 4, Name="Roman"}
            };

            Func<Student, string> func1 = new Func<Student, string>(GetStudentInfo);

            var info0 = func1(test);
            System.Console.WriteLine(info0);

            Func<Student, string> func2 = s => $"ID:{s.ID} Name:{s.Name}";

            var info1 = func2(test);
            System.Console.WriteLine(info1);

            var info2 = Select(students, s => $"ID:{s.ID} Name:{s.Name}");

            foreach (var info in info2)
            {
                System.Console.WriteLine(info);
            }

            var info3 = students.Select(s => $"ID:{s.ID} Name:{s.Name}");

            foreach (var info in info3)
            {
                System.Console.WriteLine(info);
            }

        }

        static IEnumerable<TResult> Select<TSource, TResult>(IEnumerable<TSource> sources, Func<TSource, TResult> func)
        {
            var result = new List<TResult>();
            foreach (var item in sources)
            {
                result.Add(func(item));
            }
            return result;
        }

        static string GetStudentInfo(Student stud)
        {
            var info = $"ID:{stud.ID} Name:{stud.Name}";
            return info;
        }
    }

    public static class MyExtension
    {
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, TResult> func)
        {
            var result = new List<TResult>();
            foreach (var item in sources)
            {
                result.Add(func(item));
            }
            return result;
        }
    }

    class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
