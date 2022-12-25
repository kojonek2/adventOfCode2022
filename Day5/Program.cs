namespace Day5
{
    internal class Program
    {
        private const string InputFile = "input_day5.txt";

        static void Main(string[] args)
        {
            string solutionPart1 = SolutionPart1(InputFile, 1);
            Console.WriteLine($"Solution to part1: {solutionPart1}");

            string solutionPart2 = SolutionPart1(InputFile, int.MaxValue);
            Console.WriteLine($"Solution to part2: {solutionPart2}");
        }

        private static string SolutionPart1(string filename, int craneCapacity)
        {
            List<Stack<char>> stacks = LoadStartingStacks(filename);

            IEnumerable<string> lines = File.ReadLines(filename);
            foreach (string line in lines)
            {
                if (!line.StartsWith('m'))
                    continue;

                string[] parts = line.Split();
                int count = int.Parse(parts[1]);
                int from = int.Parse(parts[3]) - 1;
                int to = int.Parse(parts[5]) - 1;

                Stack<char> craneArm = new Stack<char>();

                for (int i = 0; i < count; i++)
                {
                    if (craneArm.Count >= craneCapacity)
                    {
                        while (craneArm.Count > 0)
                            stacks[to].Push(craneArm.Pop());
                    }

                    craneArm.Push(stacks[from].Pop());
                }

                while (craneArm.Count > 0)
                    stacks[to].Push(craneArm.Pop());
            }

            string result = "";
            for (int i = 0; i < stacks.Count; i++)
            {
                result += stacks[i].Peek();
            }

            return result;
        }

        private static List<Stack<char>> LoadStartingStacks(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            List<Stack<char>> stacks = new List<Stack<char>>();
            foreach (string line in lines)
            {
                if (!line.StartsWith('['))
                    break;

                //create list
                if (stacks.Count <= 0)
                {
                    int count = (line.Length + 1) / 4;
                    for (int i = 0; i < count; i++)
                    {
                        stacks.Add(new Stack<char>());
                    }
                }

                for (int i = 0; i < stacks.Count; i++)
                {
                    int index = 1 + (4 * i);
                    if (char.IsLetter(line[index]))
                        stacks[i].Push(line[index]);
                }
            }

            //reverse stacks
            for (int i = 0; i < stacks.Count; i++)
            {
                Stack<char> previousStack = stacks[i];
                stacks[i] = new Stack<char>();

                while (previousStack.Count > 0)
                {
                    stacks[i].Push(previousStack.Pop());
                }
            }


            return stacks;
        }
    }
}