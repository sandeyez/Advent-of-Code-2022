namespace AdventOfCode
{
    class CathodeRayTube
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            // Store the value of the register X
            int registerValue = 1;

            // Store which cycle we're in
            int cycle = 1;

            int sumOfSignalStrenghts = 0;
            int add = 0;

            // Store the number of execution rounds that are left for the current operation
            int executionRounds = 0;

            for (int i = 0; i < lines.Length;)
            {
                // If we are not working on any instruction, we can read a new one
                if (executionRounds == 0)
                {
                    string line = lines[i];
                    string[] splitLine = line.Split(" ");

                    switch (splitLine[0])
                    {
                        // Instruction NOOP: Continue to the next line.
                        case "noop":
                            i++;
                            break;
                        // Instruction ADDX: Parse the number and set executionRounds to 2.
                        case "addx":
                            int number = int.Parse(splitLine[1]);
                            add = number;
                            executionRounds = 2;
                            break;
                        default:
                            break;
                    }
                }

                // Check if the cycle should count towards the sum
                if (cycle % 40 == 20)
                {
                    sumOfSignalStrenghts += (registerValue * cycle);
                }

                // Decrease the executionRounds if needed
                executionRounds = Math.Max(executionRounds - 1, 0);

                // If there are no executionRounds left and we have to add, perform the add and move to the next line
                if (executionRounds == 0 && add != 0)
                {
                    registerValue += add;
                    add = 0;
                    i++;
                }

                // Up the cycle
                cycle++;
            }

            // Print the output
            Console.WriteLine(sumOfSignalStrenghts);
            Console.ReadLine();
        }


        static void Part2(string[] lines)
        {
            // Store the value of the register X
            int registerValue = 1;

            // Store which cycle we're in
            int cycle = 1;

            int add = 0;

            // Store the number of execution rounds that are left for the current operation
            int executionRounds = 0;

            for (int i = 0; i < lines.Length;)
            {
                // If we are not working on any instruction, we can read a new one
                if (executionRounds == 0)
                {
                    string line = lines[i];
                    string[] splitLine = line.Split(" ");

                    switch (splitLine[0])
                    {
                        // Instruction NOOP: Continue to the next line.
                        case "noop":
                            i++;
                            break;
                        // Instruction ADDX: Parse the number and set executionRounds to 2.
                        case "addx":
                            int number = int.Parse(splitLine[1]);
                            add = number;
                            executionRounds = 2;
                            break;
                        default:
                            break;
                    }
                }

                // Check if the pixel is within the sprite
                if (Math.Abs(registerValue - ((cycle % 40) - 1)) <= 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }

                // Decrease the executionRounds if needed
                executionRounds = Math.Max(executionRounds - 1, 0);

                // If there are no executionRounds left and we have to add, perform the add and move to the next line
                if (executionRounds == 0 && add != 0)
                {
                    registerValue += add;
                    add = 0;
                    i++;
                }

                // End the line
                if (cycle % 40 == 0)
                {
                    Console.Write("\n");
                }

                // Up the cycle
                cycle++;
            }
            Console.ReadLine();
        }
    }
}