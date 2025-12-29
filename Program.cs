using System;

namespace Lab5
{
    class Program
    {
        //Random
        private static readonly Random random = new Random();

        //Loops until a valid number is entered (returns the number)
        private static int ReadInputToInteger()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                    return number;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number");
            }
        }

        //Loops until a positive number is entered
        static int GetPositiveInteger(string promptMessage)
        {
            int number = 0;

            do
            {
                Console.Write(promptMessage);
                number = ReadInputToInteger();

                if (number <= 0) Console.WriteLine("\n[Error]. The number has to be positive (at least 1)");
            } while (number <= 0);

            return number;
        }

        //Generates a random array with the chosen size
        static int[] GenerateRandomArray(int count)
        {
            int[] array = new int[count];

            for (int i = 0; i < count; i++)
            {
                array[i] = random.Next(10000);
            }

            return array;
        }

        //Generates an array (The user enters each number in a differrent row)
        static int[] GenerateArrayFromDifferrentRows(int count)
        {
            int[] array = new int[count];

            for (int i = 0; i < count; i++)
            {
                array[i] = ReadInputToInteger();
            }

            return array;
        }

        //Generates an array (The user eneters all the numbers in one line space-split)
        static int[]? GenerateArrayInOneRow()
        {
            string[] inputElements = Console.ReadLine().Split();
            List<int> validNumbers = new List<int>();

            foreach (string element in inputElements)
            {
                if (string.IsNullOrWhiteSpace(element))
                    continue;

                if (int.TryParse(element, out int number))
                    validNumbers.Add(number);
                else
                {
                    Console.WriteLine($"Element {element} is not a valid number");
                    return null;
                }
            }

            if (validNumbers.Count == 0)
            {
                Console.WriteLine("No valid numbers were entered");
                return null;
            }

            return validNumbers.ToArray();
        }

        //Finds the last entrance of the minimum number
        //And calculates sum of all the elements before it
        static int SumOfNumebrsBeforeLastMinNumber(int[] array)
        {
            if (array.Length == 0) return -1;

            int minNumber = array[0];
            int minLastIndex = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] <= minNumber)
                {
                    minNumber = array[i];
                    minLastIndex = i;
                }
            }

            int sum = 0;

            for (int i = 0; i < minLastIndex; i++) sum += array[i];
            
            return sum;
        }

        //Main
        static void Main(string[] args)
        {
            Console.Clear();

            //Header
            Console.WriteLine("Select an array generation method: ");
            Console.WriteLine(" 1. Randomly generated array");
            Console.WriteLine(" 2. Manual input (Each element on a new line)");
            Console.WriteLine(" 3. Manual input (All elements in a single row, split by space)");

            //Array to store the result
            int[]? finalArray = null;
            
            //Program selection loop
            while (finalArray == null)
            {
                Console.Write("Please enter your menu selection (1, 2, or 3): ");
                int input = ReadInputToInteger();

                switch (input)
                {
                    case 1: Console.WriteLine("\n\t\t- - - Random - - -"); break;
                    case 2: Console.WriteLine("\n\t\t- - - Multi-line enter - - -"); break;
                    case 3: Console.WriteLine("\n\t\t- - - One-line enter - - -"); break;
                    default: Console.WriteLine("\nInvalid input. Please enter a number between 1 and 3");
                        continue;
                }

                //Main program logic
                switch (input)
                {
                    //Generate random array
                    case 1:
                    {
                        int arrayLength = GetPositiveInteger("Enter the number of elements for the array: ");

                        finalArray = GenerateRandomArray(arrayLength);
                    }
                    break;
                    
                    //Generate array from multi-rows input
                    case 2:
                    {
                        int arrayLength = GetPositiveInteger("Enter the number of elements for the array: ");

                        finalArray = GenerateArrayFromDifferrentRows(arrayLength);
                    }
                    break;

                    //Generate array from one-row input
                    case 3:
                    {
                        do
                        {
                            Console.WriteLine("Enter the numbers for the array in one row (space-split)");
                            finalArray = GenerateArrayInOneRow();

                            if (finalArray == null)
                                Console.WriteLine("An error ocured while entering numbers. Please try again");
                        } while (finalArray == null);
                    }
                    break;
                }
            }

            //Output the result
            Console.WriteLine("Generated array: ");
            Console.WriteLine(string.Join(" ", finalArray));
            Console.WriteLine($"The sum of the numbers before the minimum is: {SumOfNumebrsBeforeLastMinNumber(finalArray)}");
        }
    }
}