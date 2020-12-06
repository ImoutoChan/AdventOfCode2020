using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day6 : DayBase
    {
        protected override void Solve()
        {
            var sumUniqueAnswers = Input
                .Split(
                    Environment.NewLine + Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(FindUniqueYesAnswers)
                .Sum(x => x.Count);


            Console.WriteLine(sumUniqueAnswers);
        }

        private static IReadOnlyCollection<char> FindUniqueYesAnswers(string groupAnswers)
        {
            return groupAnswers
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(x => x)
                .Distinct()
                .ToArray();
        }

        private static IReadOnlyCollection<char> FindAllYesAnswers(string groupAnswers)
        {
            return groupAnswers
                .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x)
                .Aggregate(
                    Enumerable.Range((int) 'a', 32).Select(x => (char) x),
                    (chars, item) => chars.Intersect(item))
                .ToArray();
        }


        protected override void SolvePart2()
        {
            
            var sumUniqueAnswers = Input
                .Split(
                    Environment.NewLine + Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(FindAllYesAnswers)
                .Sum(x => x.Count);


            Console.WriteLine(sumUniqueAnswers);
        }
    }
}
