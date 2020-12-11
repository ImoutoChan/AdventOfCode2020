using System;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day11 : DayBase
    {
        protected override void Solve()
        {
            var sits = InputStrings
                .Select(x => x.Select(y => GetState(y)).ToArray())
                .ToArray();

            var sum = FindRearrangementCount(sits, GetAdjacentCount, 4);
            Console.WriteLine(sum);
        }

        protected override void SolvePart2()
        {
            var sits = InputStrings
                .Select(x => x.Select(y => GetState(y)).ToArray())
                .ToArray();

            var sum = FindRearrangementCount(sits, GetAdjacentCount2, 5);
            Console.WriteLine(sum);
        }

        private static int FindRearrangementCount(
            SitState[][] sits,
            Func<SitState[][], int, int, int> adjacentSitsGetter,
            int limit)
        {
            var changed = false;
            SitState[][] newSits;
            do
            {
                newSits = CreateNewSits(sits);
                changed = false;
                for (var i = 0; i < sits.Length; i++)
                for (var j = 0; j < sits[0].Length; j++)
                {
                    var count = adjacentSitsGetter(sits, i, j);
                    if (count == 0 && sits[i][j] == SitState.Empty)
                    {
                        newSits[i][j] = SitState.Occupied;
                        changed = true;
                    }
                    else if (count >= limit && sits[i][j] == SitState.Occupied)
                    {
                        newSits[i][j] = SitState.Empty;
                        changed = true;
                    }
                    else
                    {
                        newSits[i][j] = sits[i][j];
                    }
                }

                sits = newSits;
            } while (changed);

            var sum = newSits.Sum(x => x.Count(y => y == SitState.Occupied));
            return sum;
        }

        private static SitState[][] CreateNewSits(SitState[][] sits)
        {
            var newSits = new SitState[sits.Length][];
            for (var i = 0; i < sits.Length; i++)
                newSits[i] = new SitState[sits[0].Length];

            return newSits;
        }

        private static int GetAdjacentCount(SitState[][] sits, int row, int column)
        {
            var count = 0;
            for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
            {
                var newRow = row + i;
                var newColumn = column + j;

                if (row == newRow && newColumn == column)
                    continue;

                if (newRow >= 0 && newRow < sits.Length
                                && newColumn >= 0 && newColumn < sits[0].Length
                                && sits[newRow][newColumn] == SitState.Occupied)
                    count++;
            }

            return count;
        }

        private static int GetAdjacentCount2(SitState[][] sits, int row, int column)
        {
            var count = 0;
            for (var i = -1; i < 2; i++)
            for (var j = -1; j < 2; j++)
            {
                var newRow = row;
                var newColumn = column;

                do
                {
                    newRow += i;
                    newColumn += j;

                    if (row == newRow && newColumn == column)
                        break;

                    if (newRow < 0 || newRow >= sits.Length || newColumn < 0 || newColumn >= sits[0].Length)
                        break;

                    if (sits[newRow][newColumn] == SitState.Occupied)
                    {
                        count++;
                        break;
                    }

                    if (sits[newRow][newColumn] == SitState.Empty) break;
                } while (true);
            }

            return count;
        }

        private SitState GetState(in char c)
        {
            return c switch
            {
                'L' => SitState.Empty,
                '.' => SitState.Floor,
                _ => throw new NotSupportedException()
            };
        }

        private enum SitState
        {
            Floor,
            Empty,
            Occupied
        }
    }
}
