using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day9 : DayBase
    {
        protected override void Solve()
        {
            var numbers = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToList();

            var wrongNumber = FindWrongNumber(numbers);

            Console.WriteLine(wrongNumber);
        }

        protected override void SolvePart2()
        {
            var numbers = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            var number = FindWrongNumber(numbers);

            for (int i = 0; i < numbers.Length; i++)
            {
                var sum = numbers[i];
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    sum += numbers[j];

                    if (sum > number)                    
                        break;

                    if (sum == number)
                    {
                        var range = numbers[i..(j + 1)];
                        var result = range.Max() + range.Min();
                        Console.WriteLine(result);
                    }
                }
            }
        }

        private static long FindWrongNumber(IReadOnlyList<long> numbers)
        {
            long wrongNumber = 0;
            for (int i = 25; i < numbers.Count; i++)
            {
                var valid = false;
                var currentNumber = numbers[i];
                for (int j = i - 25; j < i; j++)
                {
                    for (int k = j; k < i; k++)
                    {
                        if (numbers[k] + numbers[j] == currentNumber)
                            valid = true;

                    }
                }

                if (!valid)
                {
                    wrongNumber = numbers[i];
                    break;
                }
            }

            return wrongNumber;
        }
    }
}
