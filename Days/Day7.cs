using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
    internal class Day7 : DayBase
    {
        private readonly Regex _nameRegex = new(@"(?<outerbag>.+)\sbags\scontain", RegexOptions.Compiled);
        private readonly Regex _innerBagsRegex = new(@"(?<number>\d+)\s(?<innerbag>[\w\s]+)\sbag[s]{0,1}[,\s\.]{1,2}", RegexOptions.Compiled);

        protected override void Solve()
        {
            var bags = GetBags();

            var stack = new Stack<string>(bags.Count);
            var visited = new HashSet<string>(bags.Count);

            stack.Push("shiny gold");

            while (stack.Any())
            {
                var current = stack.Pop();
                visited.Add(current);
                var newBags = bags.Where(x => x.innerBags.Any(x => x.Color == current));
                foreach (var newBag in newBags.Where(x => !visited.Contains(x.Color)).Select(x => x.Color).Distinct())
                {
                    stack.Push(newBag);
                }
            }

            Console.WriteLine(visited.Count - 1);
        }

        protected override void SolvePart2()
        {
            var bags = GetBags().ToDictionary(x => x.Color, x => x);

            var result = CountForBagColor("shiny gold");
            Console.WriteLine(result - 1);

            int CountForBagColor(string color) 
            {
                var result = bags[color].innerBags.Sum(x => x.Number * CountForBagColor(x.Color)) + 1;
                return result;
            };
        }

        private IReadOnlyCollection<Bag> GetBags()
        {
            return Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(SelectBags)
                .ToList();
        }

        private Bag SelectBags(string description)
        {
            var name = _nameRegex.Match(description).Groups["outerbag"].Value;
            var innerBags = _innerBagsRegex
                .Matches(description)
                .Select(x => new Bag(
                    x.Groups["innerbag"].Value, 
                    int.Parse(x.Groups["number"].Value), 
                    Array.Empty<Bag>()))
                .ToList();

            return new(name, 1, innerBags);
        }

        private record Bag(string Color, int Number, IReadOnlyCollection<Bag> innerBags);
    }
}
