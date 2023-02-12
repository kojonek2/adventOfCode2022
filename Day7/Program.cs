namespace Day7
{
    internal class Program
    {
        private class FileDescription
        {
            public string Name { get; set; } = "";
            public int Size { get; set; }
        }

        private class DirectoryDescription
        {
            public string Name { get; set; } = "";

            public DirectoryDescription? Parent { get; set; }

            public Dictionary<string, DirectoryDescription> Directories { get; } = new Dictionary<string, DirectoryDescription>();
            public Dictionary<string, FileDescription> Files { get; } = new Dictionary<string, FileDescription>();
        }

        private const string INPUT_FILE = "input_day7.txt";
        private const int FILESYSTEM_SIZE = 70_000_000;
        private const int UPDATE_SIZE = 30_000_000;

        static void Main(string[] args)
        {
            int solutionPart1 = SolutionPart1();
            Console.WriteLine($"Solution to part1: {solutionPart1}");

            int solutionPart2 = SolutionPart2();
            Console.WriteLine($"Solution to part2: {solutionPart2}");
        }

        private static int SolutionPart1()
        {
            DirectoryDescription mainDirectory = LoadFileSystem(INPUT_FILE);
            Dictionary<DirectoryDescription, int> directoryToSize = CalculateDirectorySizes(mainDirectory);

            int sizeSum = 0;
            foreach (var pair in directoryToSize)
            {
                if (pair.Value > 100_000)
                    continue;

                sizeSum += pair.Value;
            }

            return sizeSum;
        }

        private static int SolutionPart2()
        {
            DirectoryDescription mainDirectory = LoadFileSystem(INPUT_FILE);
            Dictionary<DirectoryDescription, int> directoryToSize = CalculateDirectorySizes(mainDirectory);

            int fileSystemSize = directoryToSize[mainDirectory];
            int freeSpace = FILESYSTEM_SIZE - fileSystemSize;
            int spaceNeededToFreeUp = UPDATE_SIZE - freeSpace;

            List<DirectoryDescription> directoriesLargeEnough = new List<DirectoryDescription>();
            foreach (var pair in directoryToSize)
            {
                if (pair.Value >= spaceNeededToFreeUp)
                    directoriesLargeEnough.Add(pair.Key);
            }

            int siezeOfDeletedDirectory = directoriesLargeEnough
                .OrderBy(d => directoryToSize[d])
                .Select(d => directoryToSize[d])
                .First();
            return siezeOfDeletedDirectory;
        }

        private static Dictionary<DirectoryDescription, int> CalculateDirectorySizes(DirectoryDescription mainDirectory)
        {
            Dictionary<DirectoryDescription, int> directoryToSize = new Dictionary<DirectoryDescription, int>();

            Stack<DirectoryDescription> toProcess = new Stack<DirectoryDescription>();
            toProcess.Push(mainDirectory);

            while (toProcess.Count > 0)
            {
                DirectoryDescription directory = toProcess.Peek();

                int size = 0;
                bool allSubDirectoriesCalculated = true;
                foreach (DirectoryDescription subDirectory in directory.Directories.Values)
                {
                    if (!directoryToSize.TryGetValue(subDirectory, out int subDirectorySize))
                    {
                        allSubDirectoriesCalculated = false;
                        toProcess.Push(subDirectory);
                    }
                    else
                    {
                        size += subDirectorySize;
                    }
                }

                if (allSubDirectoriesCalculated)
                {
                    foreach (FileDescription file in directory.Files.Values)
                    {
                        size += file.Size;
                    }

                    toProcess.Pop();
                    directoryToSize[directory] = size;
                }
            }

            return directoryToSize;
        }

        private static DirectoryDescription LoadFileSystem(string filename)
        {
            IEnumerable<string> lines = File.ReadLines(filename);

            bool listingFiles = false;

            DirectoryDescription mainDirectory = new DirectoryDescription() { Name = "/" };

            DirectoryDescription? directory = null;
            foreach (string line in lines)
            {
                if (line.StartsWith("$"))
                {
                    //comands
                    string[] parts = line.Split();
                    if (parts.Length < 2)
                        throw new InvalidOperationException();

                    listingFiles = false;
                    switch (parts[1])
                    {
                        case "cd":
                            if (parts.Length < 3)
                                throw new InvalidOperationException();

                            string target = parts[2];

                            if (target == "/")
                            {
                                directory = mainDirectory;
                            }
                            else if (target == "..")
                            {
                                if (directory == null || directory.Parent == null)
                                    throw new InvalidOperationException();

                                directory = directory.Parent;
                            }
                            else
                            {
                                if (directory == null || !directory.Directories.TryGetValue(target, out DirectoryDescription? targetDir))
                                    throw new InvalidOperationException();

                                directory = targetDir;
                            }
                            break;

                        case "ls":
                            listingFiles = true;
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    //reading contents of directory
                    if (!listingFiles || directory == null)
                        throw new InvalidOperationException();

                    string[] parts = line.Split();
                    if (parts.Length < 2)
                        throw new InvalidOperationException();

                    string name = parts[1];
                    if (parts[0] == "dir")
                    {
                        if (!directory.Directories.ContainsKey(name))
                        {
                            DirectoryDescription newDirectory = new DirectoryDescription()
                            {
                                Name = name,
                                Parent = directory
                            };
                            directory.Directories[name] = newDirectory;
                        }
                    }
                    else if (int.TryParse(parts[0], out int size))
                    {
                        if (!directory.Files.ContainsKey(name))
                        {
                            FileDescription newFile = new FileDescription()
                            { 
                                Name = name,
                                Size = size,
                            };
                            directory.Files[name] = newFile;
                        }
                    }
                    else
                        throw new InvalidOperationException();
                }
            }

            return mainDirectory;
        }
    }
}