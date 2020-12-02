using System;
using System.Linq;

namespace AdventOfCode2020
{
    internal class Day1 : DayBase
    {
        protected override void Solve()
        {
            var numbers = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToArray();

            for (int i = 0; i < numbers.Length; i++)
            for (int j = i + 1; j < numbers.Length; j++)
            {
                var sum = numbers[i] + numbers[j];
                if (sum == 2020)
                {
                    Console.WriteLine(numbers[i] * numbers[j]);
                    return;
                }
                if (sum > 2020)
                    break;
            }
        }
        
        protected override void SolvePart2()
        {
            var numbers = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToArray();

            for (int i = 0; i < numbers.Length; i++)
            for (int j = i + 1; j < numbers.Length; j++)
            {
                for (int z = j + 1; z < numbers.Length; z++)
                {
                    var sum = numbers[i] + numbers[j] + numbers[z];
                    if (sum == 2020)
                    {
                        Console.WriteLine(numbers[i] * numbers[j] * numbers[z]);
                        return;
                    }

                    if (sum > 2020)
                        break;
                }

                if (numbers[i] + numbers[j] > 2020)
                    break;
            }

        }
    }
}