namespace Day8
{
    internal class Program
    {
        private const string INPUT_FILE = "input_day8.txt";

        static void Main(string[] args)
        {
            int solutionPart1 = SolutionPart1(INPUT_FILE);
            Console.WriteLine($"Solution to part1: {solutionPart1}");

            int solutionPart2 = SolutionPart2(INPUT_FILE);
            Console.WriteLine($"Solution to part2: {solutionPart2}");
        }

        private static int SolutionPart1(string inputFile)
        {
            int[,] map = LoadMap(inputFile);
            bool[,] counted = new bool[map.GetLength(0), map.GetLength(1)];

            int visibleTrees = 0;

            //from left to right
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int previousMaxHeight = -1;
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    ProcessTree(x, y, map, counted, ref previousMaxHeight, ref visibleTrees);
                }
            }

            //from right to left
            for (int y = 0; y < map.GetLength(1); y++)
            {
                int previousMaxHeight = -1;
                for (int x = map.GetLength(0) - 1; x >= 0; x--)
                {
                    ProcessTree(x, y, map, counted, ref previousMaxHeight, ref visibleTrees);
                }
            }

            //from top to bottom
            for (int x = 0; x < map.GetLength(0); x++)
            {
                int previousMaxHeight = -1;
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    ProcessTree(x, y, map, counted, ref previousMaxHeight, ref visibleTrees);
                }
            }

            //from bottom to top
            for (int x = 0; x < map.GetLength(0); x++)
            {
                int previousMaxHeight = -1;
                for (int y = map.GetLength(1) - 1; y >= 0; y--)
                {
                    ProcessTree(x, y, map, counted, ref previousMaxHeight, ref visibleTrees);
                }
            }

            string debug = "";

            for (int y = 0; y < counted.GetLength(1); y++)
            {
                for (int x = 0; x < counted.GetLength(0); x++)
                {
                    debug += counted[x, y] ? 'X' : '0';
                }

                debug += "\n";
            }

            File.WriteAllText("debug.txt", debug);

            return visibleTrees;
        }

        private static void ProcessTree(int x, int y, int[,] map, bool[,] counted, ref int previousMaxHeight, ref int visibleTrees)
        {
            if (previousMaxHeight < map[x, y])
            {
                if (!counted[x, y])
                {
                    visibleTrees++;
                    counted[x, y] = true;
                }

                previousMaxHeight = map[x, y];
            }
        }

        private static int SolutionPart2(string inputFile)
        {
            int[,] map = LoadMap(inputFile);

            int topScore = 0;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int score = GetScenicScore(x, y, map);
                    topScore = Math.Max(topScore, score);
                }
            }

            return topScore;
        }

        private static int GetScenicScore(int x, int y, int[,] map)
        {
            //top
            int top = 0;
            while (y - (top + 1) >= 0)
            {
                top++;
                if (map[x, y - top] >= map[x, y])
                {
                    break;
                }
            }

            //bottom
            int bottom = 0;
            while (y + (bottom + 1) < map.GetLength(1))
            {
                bottom++;
                if (map[x, y + bottom] >= map[x, y])
                {
                    break;
                }
            }

            //left
            int left = 0;
            while (x - (left + 1) >= 0)
            {
                left++;
                if (map[x - left, y] >= map[x, y])
                {
                    break;
                }
            }

            //right
            int right = 0;
            while (x + (right + 1) < map.GetLength(0))
            {
                right++;
                if (map[x + right, y] >= map[x, y])
                {
                    break;
                }
            }

            return top * bottom * left * right;
        }

        private static int[,] LoadMap(string inputFile)
        {
            int[,] result = new int[0,0];

            string[] lines = File.ReadAllLines(inputFile);

            for (int y = 0; y < lines.Length; y++)
            {
                char[] chars = lines[y].ToCharArray();

                if (y == 0)
                    result = new int[chars.Length, lines.Length];

                for (int x = 0; x < chars.Length; x++)
                    result[x, y] = chars[x] - '0';
            }

            return result;
        }
    }
}