using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day4 : Day
    {
        protected override string Input => "41";

        public override async Task<long> Solve()
        {
            var file = await InputReader.ReadToEndAsync();

            var passports = file.Split($"{Environment.NewLine}{Environment.NewLine}");

            return passports.Count(passport => IsValid(passport));
        }

        private static Regex fieldPattern = new Regex(@"(?<field>\w{3}):(?<value>[^\s]+)", RegexOptions.Compiled);

        private bool IsValid(string passport)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(passport);
            Console.WriteLine("");
            Console.WriteLine("------------------");
            Console.ResetColor();

            var isValid = true;

            var matches = fieldPattern.Matches(passport);

            var requiredGroups = new List<string>
            {
                "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"
            };

            foreach (Match match in matches)
            {
                var field = match.Groups["field"].Value;
                var value = match.Groups["value"].Value;

                if (!ValidateField(field, value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    isValid = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"{field}: {value}");

                requiredGroups.Remove(field);
            }

            return isValid && requiredGroups.Count == 0;
        }

        private bool ValidateField(string field, string value)
        {
            switch (field)
            {
                case "byr":
                    return ValidateYear(value, 1920, 2002);
                case "iyr":
                    return ValidateYear(value, 2010, 2020);
                case "eyr":
                    return ValidateYear(value, 2020, 2030);
                case "hgt":
                    return ValidateHeight(value);
                case "hcl":
                    return ValidateHairColor(value);
                case "ecl":
                    return ValidateEyeColor(value);
                case "pid":
                    return ValidatePassportId(value);
                default:
                    return true;
            }
        }

        private bool ValidateYear(string value, int min, int max)
        {
            return int.TryParse(value, out var byr) && byr >= min && byr <= max;
        }

        private static Regex heightRegex = new Regex(@"(?<val>\d+)(?<unit>(in|cm))", RegexOptions.Compiled);

        private bool ValidateHeight(string value)
        {
            var match = heightRegex.Match(value);
            if (match.Success)
            {
                var val = int.Parse(match.Groups["val"].Value);
                switch (match.Groups["unit"].Value)
                {
                    case "in":
                        return val >= 59 && val <= 76;
                    default:
                        return val >= 150 && val <= 193;
                }
            }
            return false;
        }

        private static Regex hairColorRegex = new Regex(@"^#[0-9a-f]{6}$");

        private bool ValidateHairColor(string value)
        {
            return hairColorRegex.IsMatch(value);
        }

        private bool ValidateEyeColor(string value)
        {
            return new[]
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
            }.Contains(value);
        }

        private static Regex passportIdRegex = new Regex(@"^\d{9}$");

        private bool ValidatePassportId(string value)
        {
            return passportIdRegex.IsMatch(value);
        }
    }
}