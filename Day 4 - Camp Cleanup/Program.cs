namespace AdventOfCode
{
    class CampCleanup
    {

        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }


        static void Part1(string[] lines)
        {
            int counter = 0;

            foreach (string line in lines)
            {
                // Split the string into two sections
                string[] sectionIDs = line.Split(",");

                // Split each section into two ID's
                int[] elf1IDs = Array.ConvertAll(sectionIDs[0].Split("-"), s => int.Parse(s));
                int[] elf2IDs = Array.ConvertAll(sectionIDs[1].Split("-"), s => int.Parse(s));

                // Check if the 1st section is contained in the second
                if (elf1IDs[0] >= elf2IDs[0] && elf1IDs[1] <= elf2IDs[1])
                {
                    counter++;
                    continue;
                }

                // Check if the 2nd section is contained in the first
                if (elf2IDs[0] >= elf1IDs[0] && elf2IDs[1] <= elf1IDs[1])
                {
                    counter++;
                }


            }

            // Print the counter
            Console.WriteLine(counter);
            Console.ReadLine();
        }

        static void Part2(string[] lines)
        {
            int counter = 0;

            foreach (string line in lines)
            {
                // Split the string into two sections
                string[] sectionIDs = line.Split(",");

                // Split each section into two ID's
                int[] elf1IDs = Array.ConvertAll(sectionIDs[0].Split("-"), s => int.Parse(s));
                int[] elf2IDs = Array.ConvertAll(sectionIDs[1].Split("-"), s => int.Parse(s));

                // Check if the 1st section's start is contained within the 2nd section
                if (elf1IDs[0] >= elf2IDs[0] && elf1IDs[0] <= elf2IDs[1])
                {
                    counter++;
                    continue;
                }

                // Check if the 2nd section's start is contained within the 1st section
                if (elf2IDs[0] >= elf1IDs[0] && elf2IDs[0] <= elf1IDs[1])
                {
                    counter++;
                }
            }
            // Print the count
            Console.WriteLine(counter);
            Console.ReadLine();
        }
    }
}