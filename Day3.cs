using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day3 : Day
    {
        protected override string Input => "31";

        public override async Task<long> Solve()
        {
            var forest = await ReadForest();

            var paths = new[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            var tasks = paths.Select(path => Traverse(forest, path));

            var result = await Task.WhenAll(tasks);

            return result.Aggregate(1L, (agg, r) => agg * r);
        }

        private Task<long> Traverse(bool[][] forest, (int right, int down) path)
        {
            var nofTrees = 0L;

            var x = 0;
            for (var y = 0; y < forest.Length; y += path.down)
            {
                var row = forest[y];
                if (row[x % row.Length])
                {
                    nofTrees++;
                }
                x += path.right;
            }
            return Task.FromResult(nofTrees);
        }

        private async Task<bool[][]> ReadForest()
        {
            var forest = new List<bool[]>();

            string line;
            while ((line = await InputReader.ReadLineAsync()) != null)
            {
                forest.Add(line.Select(c => c == '#').ToArray());
            }

            return forest.ToArray();
        }
    }
}