using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day2 : Day
    {
        protected override string Input => "21";

        public override async Task<long> Solve()
        {
            var path = Path.Join("input", $"{Input}.txt");
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            using var reader = new StreamReader(stream);

            string line;
            var count = 0;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var policy = Policy.Create(line);

                var isValid = policy.Validate2();

                Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(line);

                if (isValid)
                {
                    count++;
                }
            }
            return count;
        }

        private struct Policy
        {
            private static Regex regex = new Regex(@"(?<min>\d+)\-(?<max>\d+)\s+(?<char>\w):\s+(?<password>\w+)", RegexOptions.Compiled);

            public int Min { get; set; }
            public int Max { get; set; }
            public char Char { get; set; }
            public string Password { get; set; }

            public static Policy Create(string line)
            {
                var match = regex.Match(line);
                return new Policy
                {
                    Min = int.Parse(match.Groups["min"].Value),
                    Max = int.Parse(match.Groups["max"].Value),
                    Char = match.Groups["char"].Value[0],
                    Password = match.Groups["password"].Value
                };
            }

            public bool Validate()
            {
                var r2 = new Regex($"{Char}");
                var modified = r2.Replace(Password, "");
                var nofOccurrences = Password.Length - modified.Length;
                return nofOccurrences >= Min && nofOccurrences <= Max;
            }

            public bool Validate2()
            {
                var r2 = new Regex($@"^.{{{Min - 1}}}{Char}.{{{Max - Min - 1}}}[^{Char}]|^.{{{Min - 1}}}[^{Char}].{{{Max - Min - 1}}}{Char}");

                return r2.IsMatch(Password);
            }
        }
    }
}