using System;
using System.Linq;
using AdventOfCode2020.Days;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var days = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(DayBase)))
                .Where(x => !x.IsAbstract)
                .OrderBy(x => int.Parse(x.Name.Substring(3)))
                .Select(x => Activator.CreateInstance(x))
                .Cast<DayBase>()
                .ToList();

            foreach (var day in days) 
                day.Run();
        }
    }
}
