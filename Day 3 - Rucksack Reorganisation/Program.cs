namespace AdventOfCode
{
    class RucksackReorganisation
    {
         static void Main(string[] args)
         {
                string path = @"../../../input.txt";
                string[] lines = System.IO.File.ReadAllLines(path);

                Part2(lines);
         }

        static void Part1(string[] lines)
        {
            int sum = 0;

            foreach (string line in lines)
            {
                // Split the rucksack in the two compartments
                (string part1, string part2) = SplitCompartments(line);

                // Find all the common elements between the two parts.
                IEnumerable<char> commonElements = part1.Intersect(part2);

                // Score all the common elements between the two compartments
                foreach (char c in commonElements)
                {
                    sum += scoreElement(c);
                }
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        static Tuple<string, string> SplitCompartments(string rucksack)
        {
            int length = rucksack.Length;

            string part1 = "";
            string part2 = "";

            for(int i = 0; i < length;  i++)
            {
                char c = rucksack[i];

                if (i < length/2)
                {
                    part1 += c;
                }
                else
                {
                    part2 += c;
                }
            }

            return new Tuple<string, string>(part1, part2);
        }

        static int scoreElement(char c)
        {
            if (char.IsUpper(c))
            {
                return c - 38;
            }
            else
            {
                return c - 96;
            }
        }

        static void Part2(string[] lines)
        {
            int sum = 0;

            // Store the possible badge candidates for each round.
            IEnumerable<char> badgeCandidates = "";

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Relative index within the group of three elves: 0,1,2
                int relativeGroupIndex = i % 3;

                // If the relativeGroupIndex is 0, the badge candidates is the entire rucksack 
                if (relativeGroupIndex == 0)
                {
                    badgeCandidates = line;
                }
                // If the relativeGroupIndex is 1 or 2, we have to take the intersect of the previous bags with the current one.
                else
                {
                    badgeCandidates = badgeCandidates.Intersect(line);
                }

                // If we're at the last item for this elf group, we should score the badge and add it to the sum.
                if (relativeGroupIndex == 2)
                {
                    foreach (char c in badgeCandidates)
                    {
                        sum += scoreElement(c);
                    }
                }
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}