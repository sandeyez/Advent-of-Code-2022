namespace AdventOfCode
{
    class TreetopTreeHouse
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            // Initialize a grid to store all the tree heights
            int[,] grid = new int[lines[0].Length, lines.Length];

            // Initialize a grid to store the highest value for a point in the given direction, seen so far.
            int[,] coordinateTops = new int[lines[0].Length, lines.Length];

            // Read the input and put it in the grid
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    grid[x, y] = line[x];
                }
            }

            // Initialize a HashSet to store all the unique coordinates of visible trees
            HashSet<(int, int)> visibleTrees = new HashSet<(int, int)>();

            int gridWidth = grid.GetLength(1);
            int gridHeight = grid.GetLength(0);

            // First, start looking at the trees that are visible from above
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    int height = grid[x, y];

                    // If the tree is at the top row, it's always visible
                    if (y == 0)
                    {
                        coordinateTops[x, y] = height;
                        visibleTrees.Add((x, y));
                        continue;
                    }

                    // Check if the height of the tree is higher than all trees on top of it 
                    if (height > coordinateTops[x, y - 1])
                    {
                        // If it is, set the coordinateTops and add it to the visibleTrees array
                        coordinateTops[x, y] = height;
                        visibleTrees.Add((x, y));
                    }
                    // If a tree is not the highest in its column so far, set the highest height to the previous highest height
                    else
                    {
                        coordinateTops[x, y] = coordinateTops[x, y - 1];
                    }
                }
            }

            // Clear the coordinateTops-array for new usage
            coordinateTops = new int[lines[0].Length, lines.Length];

            // Next, start looking for trees that are visible from the right
            for (int y = 0; y < gridHeight; y++)
            {
                // Note that we will now traverse rows right to left
                for (int x = gridWidth - 1; x >= 0; x--)
                {
                    int height = grid[x, y];

                    // If the tree is at the rightmost column, it's always visible
                    if (x == gridWidth - 1)
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x,y))) {
                            visibleTrees.Add((x, y));
                        }

                        continue;
                    }

                    // Check if the tree is higher than all preceding trees at its right
                    if (height > coordinateTops[x+1, y])
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x, y)))
                        {
                            visibleTrees.Add((x, y));
                        }
                    }
                    else
                    {
                        coordinateTops[x, y] = coordinateTops[x + 1, y];
                    }
                }
            }

            // Clear the coordinateTops-array for new usage
            coordinateTops = new int[lines[0].Length, lines.Length];

            // Next, start looking for trees that are visible from the bottom
            // Note that we traverse columns bottom up
            for (int y = gridHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    int height = grid[x, y];

                    // If the tree is at the bottom of the grid, it's always visible
                    if (y == gridHeight - 1)
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x, y)))
                        {
                            visibleTrees.Add((x, y));
                        }

                        continue;
                    }

                    // Check if the tree is higher than all preceding trees at its bottom
                    if (height > coordinateTops[x, y + 1])
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x, y)))
                        {
                            visibleTrees.Add((x, y));
                        }
                    }
                    else
                    {
                        coordinateTops[x, y] = coordinateTops[x, y+1];
                    }
                }
            }

            // Clear the coordinateTops-array for new usage
            coordinateTops = new int[lines[0].Length, lines.Length];

            // Finally, start looking for trees that are visible from the left
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    int height = grid[x, y];

                    // If the tree is at the leftmost column, it's always visible
                    if (x == 0)
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x, y)))
                        {
                            visibleTrees.Add((x, y));
                        }

                        continue;
                    }

                    // Check if the tree is higher than all the preceding trees to its left
                    if (height > coordinateTops[x - 1, y])
                    {
                        coordinateTops[x, y] = height;

                        if (!visibleTrees.Contains((x, y)))
                        {
                            visibleTrees.Add((x, y));
                        }
                    }
                    else
                    {
                        coordinateTops[x, y] = coordinateTops[x - 1, y];
                    }
                }
            }

            // Print the amount of unique visible trees
            Console.WriteLine(visibleTrees.Count);
            Console.ReadLine();
        }

        static void Part2(string[] lines)
        {
            // Initialize a grid to store all the tree heights
            int[,] grid = new int[lines[0].Length, lines.Length];

            // Initialize a grid to store the highest value for a point in the given direction, seen so far.
            int[,] coordinateTops = new int[lines[0].Length, lines.Length];

            // Read the input and put it in the grid
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    grid[x, y] =line[x] - '0';
                }
            }

            int gridWidth = grid.GetLength(1);
            int gridHeight = grid.GetLength(0);

            int bestScore = 0;

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    int totalScore = 1;
                    int height = grid[x, y];

                    int viewX = x + 1;
                    int viewY = y;

                    int score = 0;
                    
                    // Get the number of trees visible to the right
                    while (viewX < gridWidth)
                    {
                        score++;
                        if (grid[viewX, y] >= height)
                        {
                            break;
                        }
                        viewX++;
                    }
                    totalScore *= score;

                    viewX = x - 1;
                    score = 0;

                    // Get the number of trees visible to the left
                    while (viewX >= 0)
                    {
                        score++;
                        if (grid[viewX, y] >= height)
                        {
                            break;
                        }
                        viewX--;
                    }
                    totalScore *= score;

                    viewY = y+1;
                    score = 0;

                    // Get the number of trees visible at the bottom
                    while (viewY < gridHeight)
                    {
                        score++;
                        if (grid[x, viewY] >= height)
                        {
                            break;
                        }
                        viewY++;
                    }
                    totalScore *= score;

                    viewY = y - 1;
                    score = 0;

                    while (viewY >= 0)
                    {
                        score++;
                        if (grid[x,viewY] >= height)
                        {
                            break;
                        }
                        viewY--;
                    }
                    totalScore *= score;

                    if (totalScore > bestScore)
                    {
                        bestScore = totalScore;
                    }

                }
            }

            Console.WriteLine(bestScore);
            Console.ReadLine();
        }
    }
}