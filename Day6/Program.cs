namespace Day6
{
    internal class Program
    {
        private const string INPUT_FILENAME = "input_day6.txt";

        static void Main(string[] args)
        {
            int solutionPart1 = SolutionPart1(4);
            Console.WriteLine($"Solution to part 1: {solutionPart1}");

            int solutionPart2 = SolutionPart1(14);
            Console.WriteLine($"Solution to part 2: {solutionPart2}");
        }

        private static int SolutionPart1(int lengthOfSequence)
        {
            string input = File.ReadAllText(INPUT_FILENAME);

            List<char> buffer = new List<char>(lengthOfSequence);
            for (int i = 0; i < input.Length; i++)
            {
                char letter = input[i];

                for (int j = 0; j < buffer.Count; j++)
                {
                    if (buffer[j] == letter)
                    {
                        buffer.RemoveRange(0, j + 1);
                    }
                }

                buffer.Add(letter);
                if (buffer.Count >= lengthOfSequence)
                    return i + 1;
            }

            return -1;
        }
    }
}