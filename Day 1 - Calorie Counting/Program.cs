using System;

namespace AdventOfCode
{
    class CalorieCounting
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        { 
            // Number of calories carried by the current elf
            int currentCalories = 0;

            // Maximum number of calories carried by any elf
            int maxCalories = 0;

            // Keep reading until the End Of Input
            foreach (string line in lines)
            {
                // If there's an empty line, finish handling this elf and get ready for the next
                if (line == "")
                {
                    if (currentCalories > maxCalories)
                        maxCalories = currentCalories;
                    currentCalories = 0;
                }
                // If the line is not empty, add the amount of calories to that of the current elf
                else
                {
                    int number = int.Parse(line);
                    currentCalories += number;
                }
            }

            // Print the result
            Console.WriteLine(maxCalories);
            Console.ReadLine();
        }

        static void Part2(string[] lines) {
            // We will store the top 3 calories in an array of length 3.
            int[] top3Calories = new int[3] { 0,0,0 };

            int currentCalories = 0;

            foreach(string line in lines)
            {
                // If there's an empty line, finish handling this elf and get ready for the next
                if (line == "")
                {
                    int lowestCalories = top3Calories.Min();
                    if (currentCalories > lowestCalories)
                    {
                        int lowestCaloriesIndex = Array.IndexOf(top3Calories, lowestCalories);
                        top3Calories[lowestCaloriesIndex] = currentCalories;
                    }
                    currentCalories = 0;
                }
                // If the line is not empty, add the amount of calories to that of the current elf
                else
                {
                    int number = int.Parse(line);
                    currentCalories += number;
                }
            }

            // Print the result
            Console.WriteLine(top3Calories.Sum());
            Console.ReadLine();
        }
    }
}