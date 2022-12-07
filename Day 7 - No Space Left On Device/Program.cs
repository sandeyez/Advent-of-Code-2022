namespace AdventOfCode
{
    abstract class FileSystemItem
    {
        public string name;
        public Directory? parent;
    }

    class File : FileSystemItem
    {
        public int size;

        public File(string name, int size, Directory parent)
        {
            this.name = name;
            this.size = size;
            this.parent = parent;
        }
    }

    class Directory : FileSystemItem
    {
        public int? size;
        public List<FileSystemItem> children = new List<FileSystemItem>();

        public Directory(string name)
        {
            this.name = name;
        }
        public Directory(string name, Directory parent)
        {
            this.name = name;
            this.parent = parent;
        }
    }

    class NoSpaceLeftOnDevice
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            // Create the root directory
            Directory root = new Directory("/");

            // Save the directory the current line is working on, initialized to root
            Directory directory = root;

            // Boolean variable to store whether we're currently reading listed files in the current directory
            bool ls = false;

            // Skip the first line, because we have already created the base directory /
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitline = line.Split(" ");

                if (line == "")
                {
                    continue;
                }

                // Check if the line is a command from the user
                if (splitline[0] == "$")
                {
                    // If we're going to execute a command, ls must be set to false again
                    ls = false;

                    // Determine which command was entered
                    switch(splitline[1])
                    {
                        // Change Directory command
                        case "cd":
                            // Get the goal directory from the split line
                            string goalDirectory = splitline[2];

                            // If the goal directory is "..", we go up to the parent directory (if it exists)
                            if (goalDirectory == "..")
                            {
                                if (directory.parent == null)
                                {
                                    throw new Exception("Current directory has no parent. Can't go up further.");
                                }

                                directory = directory.parent;

                                // We are done with this line, continue with the next one.
                                continue;
                            }

                            // Find the subdirectory from the current directory with the given name.
                            Directory? goal = (Directory?) directory.children.Find(child => child.name == goalDirectory);

                            if (goal == null)
                            {
                                throw new Exception("Changing to invalid directory");
                            }

                            directory = goal;
                            break;
                        // List command
                        case "ls":
                            // Enter listing mode
                            ls = true;
                            break;
                        default:
                            throw new Exception("Invalid command detected: " + splitline[1]);
                    }
                }
                else
                {
                    // If the current line is not a command, but we're also not reading a listing, continue on the next line.
                    if (!ls)
                    {
                        continue;
                    }
                    // Check if the line is a listed directory
                    if (splitline[0] == "dir")
                    {
                        // If it is, create a new directory with the given name
                        Directory newDirectory = new Directory(splitline[1], directory);

                        // Add it as a subdirectory to the one we're currently in
                        directory.children.Add(newDirectory);
                    }
                    // If the line isn't a listed directory, it must be a file.
                    // A file consists of <filesize> <filename>
                    else
                    {
                        int fileSize = int.Parse(splitline[0]);

                        File newFile = new File(splitline[1], fileSize, directory);
                        directory.children.Add(newFile);
                    }
                }
            }

            // Traverse the root directory to find all the sizes
            List<int> sizes = new List<int>();

            getDirectorySize(root, sizes);

            // Print the combined size of all directories with a size of at most 100.000.
            Console.WriteLine(sizes.FindAll(s => s <= 100000).Sum());
            Console.ReadLine();
        }

        static int getDirectorySize(Directory directory, List<int> sizes)
        {
            int size = 0;

            // Sum the size of all children of the current directory
            foreach (FileSystemItem child in directory.children)
            {
                if (child is File)
                {
                    File fileChild = (File) child;
                    size += fileChild.size;
                }
                else
                {
                    Directory childDirectory = (Directory)child;
                    size += getDirectorySize(childDirectory, sizes);
                }
            }

            // Add the found size to the list of sizes
            sizes.Add(size);

            // Return the size of the current directory, for recursion purposes.
            return size;
        }

        static void Part2(string[] lines)
        {
            // Create the root directory
            Directory root = new Directory("/");

            // Save the directory the current line is working on, initialized to root
            Directory directory = root;

            // Boolean variable to store whether we're currently reading listed files in the current directory
            bool ls = false;

            // Skip the first line, because we have already created the base directory /
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitline = line.Split(" ");

                if (line == "")
                {
                    continue;
                }

                // Check if the line is a command from the user
                if (splitline[0] == "$")
                {
                    // If we're going to execute a command, ls must be set to false again
                    ls = false;

                    // Determine which command was entered
                    switch (splitline[1])
                    {
                        // Change Directory command
                        case "cd":
                            // Get the goal directory from the split line
                            string goalDirectory = splitline[2];

                            // If the goal directory is "..", we go up to the parent directory (if it exists)
                            if (goalDirectory == "..")
                            {
                                if (directory.parent == null)
                                {
                                    throw new Exception("Current directory has no parent. Can't go up further.");
                                }

                                directory = directory.parent;

                                // We are done with this line, continue with the next one.
                                continue;
                            }

                            // Find the subdirectory from the current directory with the given name.
                            Directory? goal = (Directory?)directory.children.Find(child => child.name == goalDirectory);

                            if (goal == null)
                            {
                                throw new Exception("Changing to invalid directory");
                            }

                            directory = goal;
                            break;
                        // List command
                        case "ls":
                            // Enter listing mode
                            ls = true;
                            break;
                        default:
                            throw new Exception("Invalid command detected: " + splitline[1]);
                    }
                }
                else
                {
                    // If the current line is not a command, but we're also not reading a listing, continue on the next line.
                    if (!ls)
                    {
                        continue;
                    }
                    // Check if the line is a listed directory
                    if (splitline[0] == "dir")
                    {
                        // If it is, create a new directory with the given name
                        Directory newDirectory = new Directory(splitline[1], directory);

                        // Add it as a subdirectory to the one we're currently in
                        directory.children.Add(newDirectory);
                    }
                    // If the line isn't a listed directory, it must be a file.
                    // A file consists of <filesize> <filename>
                    else
                    {
                        int fileSize = int.Parse(splitline[0]);

                        File newFile = new File(splitline[1], fileSize, directory);
                        directory.children.Add(newFile);
                    }
                }
            }

            // Traverse the root directory to set all the sizes
            setDirectorySize(root);

            // Declare constant values to use in calculations
            const int totalSize = 70000000;
            const int neededSize = 30000000;

            // The space not taken up by any directory is the total size - the size of all files (aka directory "/").
            int freeSpace = totalSize - (root.size ?? 0);

            int smallestSufficingSize = Int32.MaxValue;

            // Initialize a queue to take values from
            Queue<Directory> directories = new Queue<Directory>();
            directories.Enqueue(root);

            while (directories.Count > 0)
            {
                // Dequeue a directory
                Directory dir = directories.Dequeue();

                int directorySize = dir.size ?? 0;

                // Calculate the space that would be free if we deleted this directory
                int freeAfterDelete = freeSpace + (dir.size ?? 0);

                if (freeAfterDelete >= neededSize)
                {
                    if (directorySize < smallestSufficingSize)
                        smallestSufficingSize = directorySize;

                    // Enqueue all the subdirectories
                    // Note that we only do this if the deleting the parent gave enough free space
                    dir.children.FindAll(child => child is Directory)
                        .ForEach(d => directories.Enqueue((Directory)d));
                }
            }

            Console.WriteLine(smallestSufficingSize);
            Console.ReadLine();
        }


        static int setDirectorySize(Directory directory)
        {
            int size = 0;

            // Sum the size of all children of the current directory
            foreach (FileSystemItem child in directory.children)
            {
                if (child is File)
                {
                    File fileChild = (File)child;
                    size += fileChild.size;
                }
                else
                {
                    Directory childDirectory = (Directory)child;
                    size += setDirectorySize(childDirectory);
                }
            }

            // Set the size for the current directory
            directory.size = size;

            // Return the size of the current directory, for recursion purposes.
            return size;
        }
    }
}