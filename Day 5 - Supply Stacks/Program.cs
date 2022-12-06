namespace AdventOfCode
{
    class SupplyStacks
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            int length = (lines[0].Length + 1)/4;
            List<char>[] input = new List<char>[length];
            Stack<char>[] containers = new Stack<char>[length];

            foreach (string line in lines)
            {
                // Check if we're still reading a line with containers
                if (line.Contains("["))
                {
                    // Go through the entire line, checking if there's a container on that spot
                    for (int i = 0; i < length; i++)
                    {
                        // Convert i to the ith container place
                        int index = 4 * i + 1;
                        char container = line[index];

                        // If there's a container for the ith position, update the input list.
                        if (container != ' ')
                        {
                            if (input[i] == null)
                            {
                                input[i] = new List<char>();
                            }
                            input[i].Add(container);
                        }
                    }
                }
                else if (line.Length > 1 && line[1] == '1')
                {
                    // Convert the input to stacks
                    for (int i = 0; i < input.Length; i++)
                    {
                        List<char> inputElement = input[i];
                        containers[i] = new Stack<char>(inputElement.Reverse<char>());
                    }
                }
                // Check if the line is a 'move' statement
                else if (line.Length == 0 || line[0] != 'm')
                {
                    // If not, continue with the next line
                    continue;
                }
                else
                {
                    // Split the line in 'move', amount, 'from', from, 'to', to
                    string[] lineElements = line.Split(" ");

                    int amount = int.Parse(lineElements[1]);
                    int from = int.Parse(lineElements[3]) - 1;
                    int to = int.Parse(lineElements[5]) - 1;

                    // Perform the moves on the stack
                    for (int i = 0; i < amount; i++)
                    {
                        char element = containers[from].Pop();
                        containers[to].Push(element);
                    } 
                }
            }

            // Print the output to the console
            foreach(Stack<char> container in containers)
            {
                Console.Write(container.Peek());
            }
            Console.ReadLine();
        }

        static void Part2(string[] lines)
        {
            int length = (lines[0].Length + 1) / 4;
            List<char>[] input = new List<char>[length];
            Stack<char>[] containers = new Stack<char>[length];

            foreach (string line in lines)
            {
                // Check if we're still reading a line with containers
                if (line.Contains("["))
                {
                    // Go through the entire line, checking if there's a container on that spot
                    for (int i = 0; i < length; i++)
                    {
                        // Convert i to the ith container place
                        int index = 4 * i + 1;
                        char container = line[index];

                        // If there's a container for the ith position, update the input list.
                        if (container != ' ')
                        {
                            if (input[i] == null)
                            {
                                input[i] = new List<char>();
                            }
                            input[i].Add(container);
                        }
                    }
                }
                else if (line.Length > 1 && line[1] == '1')
                {
                    // Convert the input to stacks
                    for (int i = 0; i < input.Length; i++)
                    {
                        List<char> inputElement = input[i];
                        containers[i] = new Stack<char>(inputElement.Reverse<char>());
                    }
                }
                // Check if the line is a 'move' statement
                else if (line.Length == 0 || line[0] != 'm')
                {
                    // If not, continue with the next line
                    continue;
                }
                else
                {
                    // Split the line in 'move', amount, 'from', from, 'to', to
                    string[] lineElements = line.Split(" ");

                    int amount = int.Parse(lineElements[1]);
                    int from = int.Parse(lineElements[3]) - 1;
                    int to = int.Parse(lineElements[5]) - 1;

                    List<char> elementsToMove = new List<char>();

                    // Get all the elements that need to be moved
                    for (int i = 0; i < amount; i++)
                    {
                        char element = containers[from].Pop();
                        elementsToMove.Add(element);
                    }

                    // Perform the moves
                    for (int i = elementsToMove.Count -1; i >= 0; i--)
                    {
                        char element = elementsToMove[i];
                        containers[to].Push(element);
                    }
                }
            }

            // Print the output to the console
            foreach (Stack<char> container in containers)
            {
                Console.Write(container.Peek());
            }
            Console.ReadLine();
        }
    }
}