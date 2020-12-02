using System;
using System.Linq;

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
                .Select(x => Activator.CreateInstance(x))
                .Cast<DayBase>()
                .ToList();

            foreach (var day in days) 
                day.Run();
        }
    }
}
