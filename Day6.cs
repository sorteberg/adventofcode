using System;
using System.Linq;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day6 : Day
    {
        protected override string Input => "61";

        public override async Task<long> Solve()
        {
            return (await InputReader.ReadToEndAsync())
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .SelectMany(group =>
                    group.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate<string>((res, list) => string.Concat(res.Intersect(list)))
                ).Count();
        }
    }
}