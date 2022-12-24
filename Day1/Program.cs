namespace Day1
{
    internal class Program
    {
        private const string InputFile = "input_day1.txt";

        static void Main(string[] args)
        {
            int part1Asnwer = Solution(InputFile, 1);
            Console.WriteLine($"Part one asnwer: {part1Asnwer}");

            int part2Asnwer = Solution(InputFile, 3);
            Console.WriteLine($"Part two asnwer: {part2Asnwer}");
        }

        private static int Solution(string file, int numberOfElfes)
        {
            IEnumerable<string> lines = File.ReadLines(file);

            PriorityQueue<int, int> biggestCallories = new PriorityQueue<int, int>();
            int sum = 0;
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (biggestCallories.Count < numberOfElfes)
                    {
                        biggestCallories.Enqueue(sum, sum);
                    }
                    else if (biggestCallories.Peek() < sum)
                    {
                        biggestCallories.EnqueueDequeue(sum, sum);
                    }

                    sum = 0;
                }
                else
                {
                    sum += int.Parse(line);
                }
            }

            int result = 0;
            while (biggestCallories.Count > 0)
            {
                result += biggestCallories.Dequeue();
            }
            return result;
        }
    }
}