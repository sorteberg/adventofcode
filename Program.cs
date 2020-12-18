using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace adventofcode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var defaultColor = Console.ForegroundColor;
            // var answer = await new Day2().Solve();
            // var answer = await new Day3().Solve();
            // var answer = await new Day4().Solve();
            // var answer = await new Day5().Solve();
            var answer = await new Day6().Solve();
            Console.ForegroundColor = defaultColor;

            Console.WriteLine("---------------------------------------");
            Console.WriteLine(answer);

            Console.Read();
        }
    }

    abstract class Day
    {
        public Day()
        {
            InputReader = new StreamReader(new FileStream(Path.Join("input", $"{Input}.txt"), FileMode.Open, FileAccess.Read));
        }

        public abstract Task<long> Solve();

        protected async IAsyncEnumerable<string> ReadLines()
        {
            string line;
            while ((line = await InputReader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }

        protected abstract string Input { get; }

        protected StreamReader InputReader;
    }
}