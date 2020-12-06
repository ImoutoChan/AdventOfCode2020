using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
    internal class Day4 : DayBase
    {
        protected override void Solve()
        {
            var passports = Input
                .Split(
                    Environment.NewLine + Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(GetFields)
                .Select(x => new Passport(x))
                .ToArray();


            Console.WriteLine(passports.Count(x => x.IsValid));
        }


        protected override void SolvePart2()
        {
            var passports = Input
                .Split(
                    Environment.NewLine + Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(GetFields)
                .Select(x => new Passport(x))
                .ToArray();


            Console.WriteLine(passports.Count(x => x.IsValid && x.IsFieldsValid()));
        }

        private IReadOnlyDictionary<string, string> GetFields(string input)
        {
            return input
                .Split(
                    new[] {Environment.NewLine, " "},
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(':'))
                .ToDictionary(x => x[0], x => x[1]);
        }

        private class Passport
        {
            private readonly Regex _colorRegex = new Regex("#[\\da-f]{6}", RegexOptions.Compiled);
            private readonly Regex _passportRegex = new Regex("^\\d{9}$", RegexOptions.Compiled);

            public Passport(IReadOnlyDictionary<string, string> fields)
            {
                IsValid = true;
                if (fields.TryGetValue("byr", out var byr))
                    BirthYear = byr;
                else
                    IsValid = false;

                if (fields.TryGetValue("iyr", out var iyr))
                    IssueYear = iyr;
                else
                    IsValid = false;

                if (fields.TryGetValue("eyr", out var eyr))
                    ExpirationYear = eyr;
                else
                    IsValid = false;

                if (fields.TryGetValue("hgt", out var hgt))
                    Height = hgt;
                else
                    IsValid = false;

                if (fields.TryGetValue("hcl", out var hcl))
                    HairColor = hcl;
                else
                    IsValid = false;

                if (fields.TryGetValue("ecl", out var ecl))
                    EyeColor = ecl;
                else
                    IsValid = false;

                if (fields.TryGetValue("pid", out var pid))
                    PassportId = pid;
                else
                    IsValid = false;

                if (fields.TryGetValue("cid", out var cid))
                    CountryId = cid;
            }

            public string? BirthYear { get; }
            public string? IssueYear { get; }
            public string? ExpirationYear { get; }
            public string? Height { get; }
            public string? HairColor { get; }
            public string? EyeColor { get; }
            public string? PassportId { get; }
            public string? CountryId { get; }

            public bool IsValid { get; }

            public bool IsFieldsValid()
            {
                var birthYearValid =
                    BirthYear.Length == 4
                    && int.TryParse(BirthYear, out var birthYear)
                    && birthYear >= 1920
                    && birthYear <= 2002;

                var issueYearValid =
                    IssueYear.Length == 4
                    && int.TryParse(IssueYear, out var issueYear)
                    && issueYear >= 2010
                    && issueYear <= 2020;

                var expirationYearValid =
                    ExpirationYear.Length == 4
                    && int.TryParse(ExpirationYear, out var expirationYear)
                    && expirationYear >= 2020
                    && expirationYear <= 2030;

                var heightValid =
                    Height.EndsWith("cm")
                    && int.TryParse(Height.Trim('c', 'm'), out var heightCm)
                    && heightCm >= 150
                    && heightCm <= 193
                    ||
                    Height.EndsWith("in")
                    && int.TryParse(Height.Trim('i', 'n'), out var heightIn)
                    && heightIn >= 59
                    && heightIn <= 76;

                var colorValid = _colorRegex.IsMatch(HairColor);

                var eyeColorValid = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(EyeColor);

                var passportIdValid = _passportRegex.IsMatch(PassportId);

                var valid = birthYearValid
                            && issueYearValid
                            && expirationYearValid
                            && heightValid
                            && colorValid
                            && eyeColorValid
                            && passportIdValid;

                return valid;
                //Console.WriteLine($"{BirthYear} {IssueYear} {ExpirationYear} {HairColor} {EyeColor} {PassportId} {Height}");
            }
        }
    }
}
