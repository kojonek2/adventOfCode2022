namespace Day2
{
    internal class Program
    {
        private const string InputFile = "input_day2.txt";

        public enum Shape
        {
            Rock,
            Paper,
            Scicors
        }

        public enum Result
        {
            Win,
            Lose,
            Draw
        }

        private static Dictionary<Shape, Shape> winningCombination = new Dictionary<Shape, Shape>()
        {
            { Shape.Rock, Shape.Scicors },
            { Shape.Paper, Shape.Rock },
            { Shape.Scicors, Shape.Paper },
        };

        private static Dictionary<Shape, Shape> lossingCombination = winningCombination.ToDictionary(p => p.Value, p => p.Key);

        static void Main(string[] args)
        {
            int part1Solution = SolutionPart1(InputFile);
            Console.WriteLine($"Solution to part1 {part1Solution}");

            int part2Solution = SolutionPart2(InputFile);
            Console.WriteLine($"Solution to part2 {part2Solution}");
        }

        public static int SolutionPart1(string filename)
        {
            return File.ReadLines(filename)
                .Select(l => (opponent: GetShape(l[0]), you: GetShape(l[2])))
                .Select(p => GetResultPoints(p.opponent, p.you) + GetShapePoints(p.you))
                .Sum();
        }

        public static int SolutionPart2(string filename)
        {
            return File.ReadLines(filename)
                .Select(l => (opponent: GetShape(l[0]), result: GetResult(l[2])))
                .Select(l => (l.opponent, you: GetPlayedResult(l.opponent, l.result)))
                .Select(p => GetResultPoints(p.opponent, p.you) + GetShapePoints(p.you))
                .Sum();
        }

        private static Result GetResult(char letter)
        {
            switch (letter)
            {
                case 'X':
                    return Result.Lose;
                case 'Y':
                    return Result.Draw;
                case 'Z':
                    return Result.Win;
                default:
                    throw new ArgumentException();
            }
        }

        private static Shape GetPlayedResult(Shape opponent, Result result)
        {
            if (result == Result.Draw)
                return opponent;

            if (result == Result.Lose)
                return winningCombination[opponent];

            return lossingCombination[opponent];
        }

        private static Shape GetShape(char letter)
        {
            switch (letter)
            {
                case 'A':
                case 'X':
                    return Shape.Rock;
                case 'B':
                case 'Y':
                    return Shape.Paper;
                case 'C':
                case 'Z':
                    return Shape.Scicors;
                default:
                    throw new ArgumentException();
            }
        }

        private static int GetShapePoints(Shape shape)
        {
            switch (shape)
            {
                case Shape.Rock:
                    return 1;
                case Shape.Paper:
                    return 2;
                case Shape.Scicors:
                    return 3;
                default:
                    throw new NotImplementedException();
            }
        }

        private static int GetResultPoints(Shape opponent, Shape you)
        {
            if (opponent == you)
                return 3;

            if (winningCombination[you] == opponent)
                return 6;

            return 0;
        }
    }
}