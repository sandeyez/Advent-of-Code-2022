namespace AdventOfCode
{
    class TuningTrouble
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            string line = lines[0];

            // We will store the last 4 chars in a queue
            Queue<char> lastFourChars = new Queue<char>();

            for(int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                // If there aren't 4 chars in the queue yet, enqueue this one
                if (lastFourChars.Count < 4)
                {
                    lastFourChars.Enqueue(c);
                }
                // If ther are 4 chars in the queue, check if they're all unique
                if (lastFourChars.Count == 4)
                {
                    bool isUnique = lastFourChars.GroupBy(o => o).Max(g => g.Count()) == 1;

                    // If they're unique, print the result and break out of the for-loop
                    if (isUnique)
                    {
                        Console.WriteLine(i + 1);
                        break;
                    }
                    // If there are duplicates in the last 4 chars, dequeue the oldest one and continue
                    else
                    {
                        lastFourChars.Dequeue();
                    }
                }
            }
            Console.ReadLine();
        }

        static void Part2(string[] lines)
        {
            string line = lines[0];

            // We will store the last 14 chars in a queue
            Queue<char> lastFourteenChars = new Queue<char>();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                // If there aren't 14 chars in the queue yet, enqueue this one
                if (lastFourteenChars.Count < 14)
                {
                    lastFourteenChars.Enqueue(c);
                }
                // If ther are 14 chars in the queue, check if they're all unique
                if (lastFourteenChars.Count == 14)
                {
                    bool isUnique = lastFourteenChars.GroupBy(o => o).Max(g => g.Count()) == 1;

                    // If they're unique, print the result and break out of the for-loop
                    if (isUnique)
                    {
                        Console.WriteLine(i + 1);
                        break;
                    }
                    // If there are duplicates in the last 14 chars, dequeue the oldest one and continue
                    else
                    {
                        lastFourteenChars.Dequeue();
                    }
                }
            }
            Console.ReadLine();
        }
    }
}