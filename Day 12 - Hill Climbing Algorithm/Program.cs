namespace AdventOfCode
{
    class HillClimbingAlgorithm
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            // Create a grid, storing the heights of each coordinate
            int[,] grid = new int[lines[0].Length, lines.Length];

            // Store the start and ending point of the pathfinding
            (int, int) startingPoint = (0, 0);
            (int, int) endingPoint = (0, 0);

            // Iterate through the input lines and set the heights in the grid
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    int height;

                    // Set the starting point
                    if (c == 'S')
                    {
                        height = 0;
                        startingPoint = (x, y);
                    }
                    // Set the ending point
                    else if (c == 'E')
                    {
                        height = 25;
                        endingPoint = (x, y);
                    }
                    else
                    {
                        height = line[x] - 'a';
                    }

                    grid[x, y] = height;
                }
            }

            // Store all next nodes in a queue
            Queue<((int, int), int)> queue = new Queue<((int, int), int)>();

            // Store the numbers that we've already visited. 
            HashSet<(int, int)> visitedPoints = new HashSet<(int, int)>();

            // Enqueue the starting point to begin
            queue.Enqueue((startingPoint, 0));

            // Store an array with all 4 directions in which we have neighbours
            (int, int)[] directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            int minSteps = -1;

            // Keep looking until the queue is empty
            while (queue.Count > 0)
            {
                // Dequeue a point from the queue
                ((int x, int y), int steps) = queue.Dequeue();

                // If we have found the ending point, store the number of steps it took and stop looking further
                if ((x, y) == endingPoint)
                {
                    minSteps = steps;
                    break;
                }

                // Go through to all the 4 directions to find neighbours
                foreach ((int xDirection, int yDirection) in directions)
                {
                    (int neighbourX, int neighbourY) = (x + xDirection, y + yDirection);

                    // Don't visit the point if we have already seen it before
                    if (visitedPoints.Contains((neighbourX, neighbourY)))
                        continue;

                    // Don't visit the point if it's outside bounds in the X-direction
                    if (neighbourX >= grid.GetLength(0) || neighbourX < 0)
                        continue;

                    // Don't visit the point if it's outside bounds in the Y-direction
                    if (neighbourY >= grid.GetLength(1) || neighbourY < 0)
                        continue;

                    // Don't visit the point if it's not reachable from the current position
                    if (grid[neighbourX, neighbourY] > grid[x, y] + 1)
                        continue;

                    // If all 4 requirements passed, add the point to the queue, upping the steps with 1.
                    queue.Enqueue(((neighbourX, neighbourY), steps + 1));

                    // Also add it to the HashSet, this way we won't look at the same node twice
                    visitedPoints.Add((neighbourX, neighbourY));
                }
            }

            // Print the output
            Console.WriteLine(minSteps);
            Console.ReadLine();
        }

        static void Part2(string[] lines)
        {
            // Create a grid, storing the heights of each coordinate
            int[,] grid = new int[lines[0].Length, lines.Length];

            // Store the start and ending point of the pathfinding
            (int, int) endingPoint = (0, 0);

            // Iterate through the input lines and set the heights in the grid
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    int height;

                    // Set the starting point
                    if (c == 'S')
                    {
                        height = 0;
                    }
                    // Set the ending point
                    else if (c == 'E')
                    {
                        height = 25;
                        endingPoint = (x, y);
                    }
                    else
                    {
                        height = line[x] - 'a';
                    }

                    grid[x, y] = height;
                }
            }

            // Store all next nodes in a queue
            Queue<((int, int), int)> queue = new Queue<((int, int), int)>();

            // Store the numbers that we've already visited. 
            HashSet<(int, int)> visitedPoints = new HashSet<(int, int)>();

            // Enqueue the ending point to begin
            queue.Enqueue((endingPoint, 0));

            // Store an array with all 4 directions in which we have neighbours
            (int, int)[] directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            int minSteps = -1;

            // Keep looking until the queue is empty
            while (queue.Count > 0)
            {
                // Dequeue a point from the queue
                ((int x, int y), int steps) = queue.Dequeue();

                // If we have found the ending point, store the number of steps it took and stop looking further
                if (grid[x,y] == 0)
                {
                    minSteps = steps;
                    break;
                }

                // Go through to all the 4 directions to find neighbours
                foreach ((int xDirection, int yDirection) in directions)
                {
                    (int neighbourX, int neighbourY) = (x + xDirection, y + yDirection);

                    // Don't visit the point if we have already seen it before
                    if (visitedPoints.Contains((neighbourX, neighbourY)))
                        continue;

                    // Don't visit the point if it's outside bounds in the X-direction
                    if (neighbourX >= grid.GetLength(0) || neighbourX < 0)
                        continue;

                    // Don't visit the point if it's outside bounds in the Y-direction
                    if (neighbourY >= grid.GetLength(1) || neighbourY < 0)
                        continue;

                    // Don't visit the point if it's not reachable from the current position
                    if (grid[x, y] > grid[neighbourX, neighbourY] + 1)
                        continue;

                    // If all 4 requirements passed, add the point to the queue, upping the steps with 1.
                    queue.Enqueue(((neighbourX, neighbourY), steps + 1));

                    // Also add it to the HashSet, this way we won't look at the same node twice
                    visitedPoints.Add((neighbourX, neighbourY));
                }
            }

            // Print the output
            Console.WriteLine(minSteps);
            Console.ReadLine();
        }

    }
}