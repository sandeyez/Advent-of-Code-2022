using System.Collections.Generic;

namespace AdventOfCode
{
    enum Operation
    {
        PLUS,
        MULT,
    }
    class Monkey
    {
        public List<long> items;
        private (Operation, int) operation;
        public int testNumber;
        public int monkeyFalse;
        public int monkeyTrue;

        public Monkey(List<int> items, (Operation, int) operation, int testNumber, int monkeyFalse, int monkeyTrue)
        {
            this.items = items.ConvertAll(i => (long) i);
            this.testNumber = testNumber;
            this.monkeyFalse = monkeyFalse;
            this.monkeyTrue = monkeyTrue;

            this.operation = operation;
        }

        public long PerformOperation(long item)
        {
            long operand = operation.Item2 == -1 ? item : (long) operation.Item2;

            switch (operation.Item1)
            {
                case Operation.MULT:
                    return item * operand;
                case Operation.PLUS:
                    return item + operand;
                default:
                    return -1;
            }
        }

        public bool PerformTest(long item)
        {
            return item % testNumber == 0;
        }
    }
    class MonkeyInTheMiddle
    {
        static void Main(string[] args)
        {
            string path = @"../../../input.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            Part2(lines);
        }

        static void Part1(string[] lines)
        {
            // Initalize a list to store all the Monkey-objects
            List<Monkey> monkeys = new List<Monkey>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line == "")
                {
                    continue;
                }

                List<int> startingItems = new List<int>();

                // Initialize the variables with dummy values. These will all be overwritten.
                Operation operation = Operation.MULT;
                int operand = 0;
                int testNumber = -1;
                int monkeyTrue = -1, monkeyFalse = -1;

                for (int j = 0; j < 6; i++, j++)
                {
                    line = lines[i];

                    // Split the current line and find all the non-empty parts.
                    string[] splitLine = Array.FindAll(line.Split(" "), s => s != "");

                    switch (splitLine[0])
                    {
                        case "Monkey":
                            continue;
                        // Parse the starting items for this monkey
                        case "Starting":
                            string itemsString = line.Split(": ")[1];

                            startingItems = itemsString.Split(", ").ToList().ConvertAll(int.Parse);
                            break;
                        // Parse the operation for this monkey
                        case "Operation:":
                            string[] operationParts = line.Split(" = ")[1].Split(" ");
                            string _operator = operationParts[1];

                            switch (_operator)
                            {
                                case "*":
                                    operation = Operation.MULT;
                                    break;
                                case "+":
                                    operation = Operation.PLUS;
                                    break;
                                default:
                                    throw new Exception("Invalid operator: " + _operator);
                            }

                            // Check if the operation is applied on just two times the old number
                            if (operationParts[0] == operationParts[2])
                                operand = -1;
                            else
                                operand = int.Parse(operationParts[2]);

                            break;
                        // Parse the test for this monkey
                        case "Test:":
                            // The number divided by is always last in the line
                            testNumber = int.Parse(splitLine[splitLine.Length - 1]);

                            break;
                        // Parse the true/false monkeys for this monkey
                        case "If":
                            // The number of the monkey is always last on the line
                            int number = int.Parse(splitLine[splitLine.Length - 1]);

                            // Check if it concerns the true or the false monkey
                            if (splitLine[1] == "true:")
                            {
                                monkeyTrue = number;
                            }
                            else if (splitLine[1] == "false:")
                            {
                                monkeyFalse = number;
                            }

                            break;

                        default:
                            throw new Exception("Uncaught line: " + line);
                    }
                }
                // Create the monkey object with all the parsed values
                Monkey monkey = new Monkey(startingItems, (operation, operand), testNumber, monkeyFalse, monkeyTrue);

                monkeys.Add(monkey);
            }

            // Play the game

            // Store the number of inspects for each monkey, per index
            int[] numberOfInspects = new int[monkeys.Count];

            // Play 20 rounds
            for (int round = 0; round < 20; round++)
            {
                for (int j = 0; j < monkeys.Count; j++)
                {
                    Monkey monkey = monkeys[j];

                    // Update the number of inspects with the current amount of items
                    numberOfInspects[j] += monkey.items.Count;

                    for (int i = 0; i < monkey.items.Count; i++)
                    {
                        // Perform the monkey's operation to the item.
                        monkey.items[i] = monkey.PerformOperation(monkey.items[i]);

                        // Monkey gets bored with the item: divide by three.
                        monkey.items[i] /= 3;

                        // See if the current item passes the test
                        bool testSuccess = monkey.PerformTest(monkey.items[i]);

                        // Grab the item and remove it from the array
                        long item = monkey.items[i];
                        monkey.items.RemoveAt(i);

                        // The old item at place i is gone, so we have to re-check index i
                        i--;

                        // Add the item to the corresponding true- or false-monkey
                        if (testSuccess)
                        {
                            monkeys[monkey.monkeyTrue].items.Add(item);
                        }
                        else
                        {
                            monkeys[monkey.monkeyFalse].items.Add(item);
                        }
                    }
                }
            }

            (long max, long secondMax) = findTwoMaxValues(numberOfInspects);

            Console.WriteLine(max * secondMax);
            Console.ReadLine();
        }

        static (int, int) findTwoMaxValues(int[] numbers)
        {
            int firstMax = Int32.MinValue;
            int secondMax = Int32.MinValue;

            foreach (int number in numbers)
            {
                if (number > firstMax)
                {
                    secondMax = firstMax;
                    firstMax = number;
                }
                else if (number > secondMax)
                {
                    secondMax = number;
                }
            }

            return (firstMax, secondMax);
        }

        static void Part2(string[] lines)
        {
            // Initalize a list to store all the Monkey-objects
            List<Monkey> monkeys = new List<Monkey>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line == "")
                {
                    continue;
                }

                List<int> startingItems = new List<int>();

                // Initialize the variables with dummy values. These will all be overwritten.
                Operation operation = Operation.MULT;
                int operand = 0;
                int testNumber = -1;
                int monkeyTrue = -1, monkeyFalse = -1;

                for (int j = 0; j < 6; i++, j++)
                {
                    line = lines[i];

                    // Split the current line and find all the non-empty parts.
                    string[] splitLine = Array.FindAll(line.Split(" "), s => s != "");

                    switch (splitLine[0])
                    {
                        case "Monkey":
                            continue;
                        // Parse the starting items for this monkey
                        case "Starting":
                            string itemsString = line.Split(": ")[1];

                            startingItems = itemsString.Split(", ").ToList().ConvertAll(int.Parse);
                            break;
                        // Parse the operation for this monkey
                        case "Operation:":
                            string[] operationParts = line.Split(" = ")[1].Split(" ");
                            string _operator = operationParts[1];

                            switch (_operator)
                            {
                                case "*":
                                    operation = Operation.MULT;
                                    break;
                                case "+":
                                    operation = Operation.PLUS;
                                    break;
                                default:
                                    throw new Exception("Invalid operator: " + _operator);
                            }

                            // Check if the operation is applied on just two times the old number
                            if (operationParts[0] == operationParts[2])
                                operand = -1;
                            else
                                operand = int.Parse(operationParts[2]);

                            break;
                        // Parse the test for this monkey
                        case "Test:":
                            // The number divided by is always last in the line
                            testNumber = int.Parse(splitLine[splitLine.Length - 1]);

                            break;
                        // Parse the true/false monkeys for this monkey
                        case "If":
                            // The number of the monkey is always last on the line
                            int number = int.Parse(splitLine[splitLine.Length - 1]);

                            // Check if it concerns the true or the false monkey
                            if (splitLine[1] == "true:")
                            {
                                monkeyTrue = number;
                            }
                            else if (splitLine[1] == "false:")
                            {
                                monkeyFalse = number;
                            }

                            break;

                        default:
                            throw new Exception("Uncaught line: " + line);
                    }
                }
                // Create the monkey object with all the parsed values
                Monkey monkey = new Monkey(startingItems, (operation, operand), testNumber, monkeyFalse, monkeyTrue);

                monkeys.Add(monkey);
            }

            // Shout-out to reddit for this hint. Never would've thought of this myself.
            long SUPER_MODULUS = monkeys.Aggregate(1, (acc, monkey) => acc *= monkey.testNumber);

            // Play the game

            // Store the number of inspects for each monkey, per index
            int[] numberOfInspects = new int[monkeys.Count];

            // Play 10.000 rounds
            for (int round = 0; round < 10000; round++)
            {
                for (int j = 0; j < monkeys.Count; j++)
                {
                    Monkey monkey = monkeys[j];

                    // Update the number of inspects with the current amount of items
                    numberOfInspects[j] += monkey.items.Count;

                    for (int i = 0; i < monkey.items.Count; i++)
                    {
                        // Perform the monkey's operation to the item.
                        monkey.items[i] = monkey.PerformOperation(monkey.items[i]);

                        monkey.items[i] %= SUPER_MODULUS;

                        // See if the current item passes the test
                        bool testSuccess = monkey.PerformTest(monkey.items[i]);

                        // Grab the item and remove it from the array
                        long item = monkey.items[i];
                        monkey.items.RemoveAt(i);

                        // The old item at place i is gone, so we have to re-check index i
                        i--;

                        // Add the item to the corresponding true- or false-monkey
                        if (testSuccess)
                        {
                            monkeys[monkey.monkeyTrue].items.Add(item);
                        }
                        else
                        {
                            monkeys[monkey.monkeyFalse].items.Add(item);
                        }
                    }
                }
            }

            (long max, long secondMax) = findTwoMaxValues(numberOfInspects);

            Console.WriteLine(max * secondMax);
            Console.ReadLine();
        }
    }
}