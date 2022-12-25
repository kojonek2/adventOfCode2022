namespace Day3
{
    internal class Program
    {
        public const string InputFile = "input_day3.txt";

        static void Main(string[] args)
        {
            int part1Solution = SolutionPart1(InputFile);
            Console.WriteLine($"Part1 solution {part1Solution}");

            int part2Solution = SolutionPart2(InputFile);
            Console.WriteLine($"Part2 solution {part2Solution}");
        }

        private static int SolutionPart1(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            int sum = 0;
            foreach (string line in lines)
            {
                char[] items = line.ToCharArray();
                HashSet<char> firstCompartment = items.Take(items.Length / 2).ToHashSet();

                for (int i = items.Length / 2; i < items.Length; i++)
                {
                    if (firstCompartment.Contains(items[i]))
                    {
                        sum += GetItemPriority(items[i]);
                        break;
                    }
                }
            }

            return sum;
        }

        private static int SolutionPart2(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);
            int sum = 0;
            
            int i = 0;
            HashSet<char> commonItems = new HashSet<char>();
            foreach (string line in lines)
            {
                if (i == 0)
                {
                    commonItems = line.ToHashSet();
                }
                else
                {
                    commonItems.IntersectWith(line.ToCharArray());
                }

                if (i >= 2)
                {
                    sum += GetItemPriority(commonItems.First());
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            return sum;
        }

        private static int GetItemPriority(char item)
        {
            int asciCode = (int)item;

            switch (asciCode)
            {
                case >= 65 and <= 90:
                    return asciCode - 38;
                case >= 97 and <= 122:
                    return asciCode - 96;
                default:
                    throw new ArgumentException();
            }
        }
    }
}