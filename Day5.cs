using System;
using System.Linq;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day5 : Day
    {
        protected override string Input => "51";

        public override async Task<long> Solve()
        {
            var seats = await ReadLines()
                .Select(r => ParseLine(r))
                .OrderBy(r => r)
                .ToArrayAsync();

            for (var i = 1; i < seats.Length - 1; i++)
            {
                if (seats[i] - seats[i - 1] > 1) return seats[i] - 1;
            }
            return 0;
        }

        private int ParseLine(string line)
        {
            var minRow = 0;
            var maxRow = 127;

            var minColumn = 0;
            var maxColumn = 7;

            for (var i = 0; i < 7; i++)
            {
                var c = line[i];

                if (c == 'F') maxRow = maxRow - (maxRow - minRow) / 2 - 1;
                else minRow = minRow + (maxRow - minRow) / 2 + 1;

                Console.WriteLine($"{c} --> min: {minRow}, max: {maxRow}");
            }

            for (var i = 7; i < 10; i++)
            {
                var c = line[i];

                if (c == 'L') maxColumn = maxColumn - (maxColumn - minColumn) / 2 - 1;
                else minColumn = minColumn + (maxColumn - minColumn) / 2 + 1;

                Console.WriteLine($"{c} --> min: {minColumn}, max: {maxColumn}");
            }

            return maxRow * 8 + maxColumn;
        }
    }
}