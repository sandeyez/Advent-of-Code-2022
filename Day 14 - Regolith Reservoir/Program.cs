string path = @"../../../input.txt";
string[] lines = System.IO.File.ReadAllLines(path);

Part2(lines);

static void Part1(string[] lines)
{
    // Save all the xs and ys from the input to determine the size of the grid
    List<int> xs = new List<int>();
    List<int> ys = new List<int>();

    // Store the number of points per rock-group.
    // This way, we now how many points are connected in the same rock.
    List<int> wallLengths = new List<int>();

    foreach (string line in lines)
    {
        string[] points = line.Split(" -> ");
        wallLengths.Add(points.Length);

        foreach (string point in points)
        {
            string[] coordinates = point.Split(",");

            xs.Add(int.Parse(coordinates[0]));
            ys.Add(int.Parse(coordinates[1]));
        }
    }

    // Also add the sand source to the list of points. It also has to be on the grid.
    xs.Add(500); ys.Add(0);

    (int xMin, int xMax) = GetMinMaxFromList(xs);
    (int yMin, int yMax) = GetMinMaxFromList(ys);

    // Remove the sand source again. It is not a rock that has to be drawn.
    xs.RemoveAt(xs.Count - 1);
    ys.RemoveAt(ys.Count - 1);

    // Grid to store the sand and rock on specific points.
    // +1 indicates a rock, -1 indicates sand.
    // We have to add 2 extra to the width in order to allow sand to flow outside the grid.
    int[,] grid = new int[xMax - xMin + 3, yMax - yMin + 1];

    // Iterate through every rock wall.
    for (int i = 0, pointIndex = 0; i < wallLengths.Count; i++)
    {
        // Get the number of points in this rock wall.
        int wallLength = wallLengths[i];
        (int, int) prevPoint = (0, 0);

        for (int j = 0; j < wallLength; j++, pointIndex++)
        {
            int currentX = xs[pointIndex];
            int currentY = ys[pointIndex];

            // If this is the first point in the wall, we don't have a point to draw from, yet.
            if (j == 0)
            {
                prevPoint = (currentX, currentY);
                continue;
            }

            // If the current X and previous X aren't the same, we have movement in the X-direction for this wall-part.
            if (currentX != prevPoint.Item1)
                for (int x = Math.Min(prevPoint.Item1, currentX); x <= Math.Max(prevPoint.Item1, currentX); x++)
                {
                    // Convert the input values to their grid coordinates
                    int gridX = x - xMin + 1;
                    int gridY = currentY - yMin;

                    grid[gridX, gridY] = 1;
                }

            // If the current Y and previous Y aren't the same, we have movement in the Y-direction for this wall-part.
            if (currentY != prevPoint.Item2)
                for (int y = Math.Min(prevPoint.Item2, currentY); y <= Math.Max(prevPoint.Item2, currentY); y++)
                {
                    // Convert the input values to their grid coordinates
                    int gridX = currentX - xMin + 1;
                    int gridY = y - yMin;

                    grid[gridX, gridY] = 1;
                }

            prevPoint = (currentX, currentY);
        }
    }

    (int, int) sandSource = (500 - xMin + 1, 0 - yMin);
    int sandsAtRest = 0;

    bool sandFlowedInAbyss = false;

    while (!sandFlowedInAbyss)
    {
        (int, int) newSandPoint = sandSource;

        // Add the sand to the grid
        grid[newSandPoint.Item1, newSandPoint.Item2] = -1;

        bool sandFalling = true;

        while (sandFalling)
        {
            int sandX = newSandPoint.Item1;
            int sandY = newSandPoint.Item2;

            if (sandY + 1 >= grid.GetLength(1))
            {
                sandFlowedInAbyss = true;
                break;
            }

            // Check if the point down one step is free.
            if (grid[sandX, sandY + 1] == 0)
                newSandPoint = (sandX, sandY + 1);
            // Check if the point one step down and one to the left is free.
            else if (grid[sandX - 1, sandY + 1] == 0)
                newSandPoint = (sandX - 1, sandY + 1);
            // Check if the point one step down and one to the right is free.
            else if (grid[sandX + 1, sandY + 1] == 0)
                newSandPoint = (sandX + 1, sandY + 1);
            // If the sand can't move anymore, its become at rest
            else
            {
                sandsAtRest++;
                sandFalling = false;
            }

            // Check if the sand moved this round
            if (sandX != newSandPoint.Item1 || sandY != newSandPoint.Item2)
            {
                // Delete the sand from its previous place in the grid.
                grid[sandX, sandY] = 0;

                // Add the sand to its new point.
                grid[newSandPoint.Item1, newSandPoint.Item2] = -1;
            }
        }
    }

    Console.WriteLine(sandsAtRest);
    Console.ReadLine();
}

static (int, int) GetMinMaxFromList(List<int> list)
{
    int min = Int32.MaxValue;
    int max = Int32.MinValue;

    foreach (int n in list)
    {
        if (n < min)
            min = n;
        if (n > max)
            max = n;
    }

    return (min, max);
}

static void Part2(string[] lines)
{
    // Save all the xs and ys from the input to determine the size of the grid
    List<int> xs = new List<int>();
    List<int> ys = new List<int>();

    // Store the number of points per rock-group.
    // This way, we now how many points are connected in the same rock.
    List<int> wallLengths = new List<int>();

    foreach (string line in lines)
    {
        string[] points = line.Split(" -> ");
        wallLengths.Add(points.Length);

        foreach (string point in points)
        {
            string[] coordinates = point.Split(",");

            xs.Add(int.Parse(coordinates[0]));
            ys.Add(int.Parse(coordinates[1]));
        }
    }

    // Let's use a dictionary to store grid-values. This way, we are not required to know the size beforehand.
    Dictionary<(int, int), int> grid = new Dictionary<(int, int), int>();

    // Iterate through every rock wall.
    for (int i = 0, pointIndex = 0; i < wallLengths.Count; i++)
    {
        // Get the number of points in this rock wall.
        int wallLength = wallLengths[i];
        (int, int) prevPoint = (0, 0);

        for (int j = 0; j < wallLength; j++, pointIndex++)
        {
            int currentX = xs[pointIndex];
            int currentY = ys[pointIndex];

            // If this is the first point in the wall, we don't have a point to draw from, yet.
            if (j == 0)
            {
                prevPoint = (currentX, currentY);
                continue;
            }

            // If the current X and previous X aren't the same, we have movement in the X-direction for this wall-part.
            if (currentX != prevPoint.Item1)
                for (int x = Math.Min(prevPoint.Item1, currentX); x <= Math.Max(prevPoint.Item1, currentX); x++)
                {
                    grid[(x, currentY)] = 1;
                }

            // If the current Y and previous Y aren't the same, we have movement in the Y-direction for this wall-part.
            if (currentY != prevPoint.Item2)
                for (int y = Math.Min(prevPoint.Item2, currentY); y <= Math.Max(prevPoint.Item2, currentY); y++)
                {
                    grid[(currentX, y)] = 1;
                }

            prevPoint = (currentX, currentY);
        }
    }

    (int, int) sandSource = (500, 0);
    bool sandReachedSource = false;

    int sandsAtRest = 0;

    int yMax = ys.Max();

    while (!sandReachedSource)
    {
        (int, int) newSandPoint = sandSource;

        // Add the sand to the grid
        grid[(newSandPoint.Item1, newSandPoint.Item2)] = -1;

        bool sandFalling = true;

        while (sandFalling)
        {
            int sandX = newSandPoint.Item1;
            int sandY = newSandPoint.Item2;

            // Check if the point down one step is free.
            if (sandY + 1 <= yMax + + 1 && (!grid.ContainsKey((sandX, sandY + 1)) || grid[(sandX, sandY + 1)] == 0))
                newSandPoint = (sandX, sandY + 1);
            // Check if the point one step down and one to the left is free.
            else if (sandY + 1 <= yMax + 1 && (!grid.ContainsKey((sandX - 1, sandY + 1)) || grid[(sandX - 1, sandY + 1)] == 0))
                newSandPoint = (sandX - 1, sandY + 1);
            // Check if the point one step down and one to the right is free.
            else if (sandY + 1 <= yMax + 1 && (!grid.ContainsKey((sandX + 1, sandY + 1)) || grid[(sandX + 1, sandY + 1)] == 0))
                newSandPoint = (sandX + 1, sandY + 1);
            // If the sand can't move anymore, its become at rest
            else
            {
                sandFalling = false;
                sandsAtRest++;
            }

            // Check if the sand moved this round
            if (sandX != newSandPoint.Item1 || sandY != newSandPoint.Item2)
            {
                // Delete the sand from its previous place in the grid.
                grid[(sandX, sandY)] = 0;

                // Add the sand to its new point.
                grid[(newSandPoint.Item1, newSandPoint.Item2)] = -1;
            }
        }

        // Check if there's sand on the sandSource point after the sand stopped moving
        if (grid[sandSource] == -1)
        {
            break;
        }
    }

    Console.WriteLine(sandsAtRest);
    Console.ReadLine();
}