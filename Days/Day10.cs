using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day10 : DayBase
    {
        protected override void Solve()
        {
            var jolts = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToList();

            var deviceAdapter = jolts.Max() + 3;

            var orderedJolts = jolts.Append(0).Append(deviceAdapter).OrderBy(x => x).ToList();

            long[] counts = new long[4];
            for (int i = 1; i < orderedJolts.Count(); i++)
            {
                counts[orderedJolts[i] - orderedJolts[i - 1]]++;
            }
            
            Console.WriteLine(counts[1] * counts[3]);
        }

        protected override void SolvePart2()
        {
            var jolts = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToList();

            var deviceAdapter = jolts.Max() + 3;

            var orderedJolts = jolts.Append(0).Append(deviceAdapter).OrderBy(x => x).ToArray();

            var arrangementsFor = new Dictionary<int, long>();
            for (int i = 0; i < orderedJolts.Length; i++)
            {
                arrangementsFor[i] = CountArrangements(((Span<long>)orderedJolts).Slice(0, i + 1), arrangementsFor);
            }

            Console.WriteLine(arrangementsFor.Last().Value);
        }

        private static long CountArrangements(ReadOnlySpan<long> jolts, IReadOnlyDictionary<int, long> arrangements)
        {
            var currentIndex = jolts.Length - 1;
            var current = jolts[currentIndex];
            
            var count = currentIndex > 0 ? arrangements[currentIndex - 1] : 1;

            if (currentIndex >= 2 && current - jolts[currentIndex - 2] <= 3)
                count += arrangements[currentIndex - 2];

            if (currentIndex >= 3 && current - jolts[currentIndex - 3] <= 3)
                count += arrangements[currentIndex - 3];

            return count;
        }
    }
}
