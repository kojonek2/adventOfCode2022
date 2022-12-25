namespace Day4
{
    internal class Program
    {
        private const string InputFile = "input_day4.txt";

        private struct Range
        {
            public int Start;
            public int End;

            public bool Contains(Range other)
            {
                return Start <= other.Start && End >= other.End;
            }

            public bool Overlaps(Range other)
            {
                if (other.End >= Start && other.Start <= End)
                    return true;

                if (other.Start <= End && other.End >= Start)
                    return true;

                return false;
            }

            public static Range FromString(string input)
            {
                string[] numbers = input.Split('-');
                int start = int.Parse(numbers[0]);
                int end = int.Parse(numbers[1]);

                return new Range()
                {
                    Start = start,
                    End = end,
                };
            }
        }

        static void Main(string[] args)
        {
            int solutionPart1 = SolvePart1(InputFile);
            Console.WriteLine($"Solution to part1 {solutionPart1}");

            int solutionPart2 = SolvePart2(InputFile);
            Console.WriteLine($"Solution to part2 {solutionPart2}");
        }

        private static int SolvePart1(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            int sum = 0;
            foreach (string line in lines)
            {
                string[] ranges = line.Split(',');
                Range range1 = Range.FromString(ranges[0]);
                Range range2 = Range.FromString(ranges[1]);

                if (range1.Contains(range2) || range2.Contains(range1))
                    sum++;
            }

            return sum;
        }

        private static int SolvePart2(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            int sum = 0;
            foreach (string line in lines)
            {
                string[] ranges = line.Split(',');
                Range range1 = Range.FromString(ranges[0]);
                Range range2 = Range.FromString(ranges[1]);

                if (range1.Overlaps(range2))
                    sum++;
            }

            return sum;
        }
    }
}