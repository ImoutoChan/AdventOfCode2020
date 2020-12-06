using System;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day3 : DayBase
    {
        protected override void Solve()
        {
            var forest = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Select(y => y == '#').ToArray())
                .ToArray();

            var result = FindTreesOnSlope(forest, 3, 1);

            Console.WriteLine(result);
        }

        protected override void SolvePart2()
        {
            var forest = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Select(y => y == '#').ToArray())
                .ToArray();

            long multipliedResult =
                FindTreesOnSlope(forest, 1, 1) *
                FindTreesOnSlope(forest, 3, 1) *
                FindTreesOnSlope(forest, 5, 1) *
                FindTreesOnSlope(forest, 7, 1) *
                FindTreesOnSlope(forest, 1, 2);

            Console.WriteLine(multipliedResult);
        }

        private static long FindTreesOnSlope(bool[][] forest, int xDelta, int yDelta)
        {
            var x = 0;
            var y = 0;

            var xLength = forest[0].Length;
            var yLength = forest.Length;

            var count = 0;
            while (y < yLength - 1)
            {
                x = (x + xDelta) % xLength;
                y += yDelta;

                if (forest[y][x])
                    count++;
            }

            return count;
        }
    }
}