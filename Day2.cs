using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    internal class Day2 : DayBase
    {
        private static readonly Regex Regex = new Regex(
            @"(?<From>\d+)-(?<To>\d+)\s(?<Letter>\w):\s(?<Password>\w+)",
            RegexOptions.Compiled);
        
        protected override void Solve()
        {
            var result = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => GetPasswordModel(x))
                .Where(
                    x =>
                    {
                        var count = x.Password.Count(y => y == x.Letter[0]);
                        return count <= x.To && count >= x.From;
                    })
                .Count();

            Console.WriteLine(result);
        }
        
        protected override void SolvePart2()
        {
            var result = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => GetPasswordModel(x))
                .Where(
                    x =>
                    {
                        var first = x.Password[x.From - 1] == x.Letter[0];
                        var second = x.Password[x.To - 1] == x.Letter[0] ;
                        return (first || second) && !(second && first);
                    })
                .Count();

            Console.WriteLine(result);
        }

        private static (int From, int To, string Letter, string Password, string Input) GetPasswordModel(string input)
        {
            var match = Regex.Match(input);

            return (
                int.Parse(match.Groups["From"].Value),
                int.Parse(match.Groups["To"].Value),
                match.Groups["Letter"].Value,
                match.Groups["Password"].Value,
                input);
        }
    }
}