using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] files = { "Road_1_256.txt", "Road_2_256.txt", "Road_3_256.txt", "Road_1_2048.txt", "Road_2_2048.txt", "Road_3_2048.txt" };
        int[][] dataArrays = new int[files.Length][];

        for (int i = 0; i < files.Length; i++)
        {
            string[] lines = File.ReadAllLines(files[i]);
            dataArrays[i] = Array.ConvertAll(lines, int.Parse);
        }

        // Task 1 is already completed (Reading files into arrays)
        Console.WriteLine("Enter a value to search for:");
        int searchValue = int.Parse(Console.ReadLine());

        for (int i = 0; i < dataArrays.Length; i++)
        {
            Console.WriteLine($"Processing file {files[i]}");

            // Task 2: Sort in ascending and descending order and display every 10th or 50th value
            int displayInterval = (i < 3) ? 10 : 50; // For 256-length arrays use 10, for 2048-length arrays use 50
            BubbleSort(dataArrays[i], true); // Sort in ascending order
            Console.WriteLine("Sorted in ascending order:");
            DisplayEveryNthValue(dataArrays[i], displayInterval);
            BubbleSort(dataArrays[i], false); // Sort in descending order
            Console.WriteLine("Sorted in descending order:");
            DisplayEveryNthValue(dataArrays[i], displayInterval);

            // Task 3 & 4: Search for user-defined value and provide location(s) or nearest value(s)
            List<int> foundIndices = LinearSearch(dataArrays[i], searchValue);

            if (foundIndices.Count > 0)
            {
                Console.WriteLine($"Value {searchValue} found at index/indices: {string.Join(", ", foundIndices)}");
            }
            else
            {
                Console.WriteLine($"Value {searchValue} not found.");
                KeyValuePair<int, int> nearestValueInfo = FindNearestValue(dataArrays[i], searchValue);
                Console.WriteLine($"Nearest value is {nearestValueInfo.Key} at index {nearestValueInfo.Value}");
            }
        }

        // Task 6 & 7: Merge the files and repeat tasks 2 to 4
        int[] merged256 = MergeArrays(dataArrays[0], dataArrays[2]);
        int[] merged2048 = MergeArrays(dataArrays[3], dataArrays[5]);

        for (int i = 0; i < 2; i++)
        {
            int[] mergedArray = (i == 0) ? merged256 : merged2048;
            string mergedFileName = (i == 0) ? "Merged_256.txt" : "Merged_2048.txt";
            Console.WriteLine($"Processing merged file {mergedFileName}");

            int displayInterval = (i == 0) ? 10 : 50;
            BubbleSort(mergedArray, true); // Sort in ascending order
            Console.WriteLine("Sorted in ascending order:");
            DisplayEveryNthValue(mergedArray, displayInterval);
            BubbleSort(mergedArray, false); // Sort in descending order
            Console.WriteLine("Sorted in descending order:");
            DisplayEveryNthValue(mergedArray, displayInterval);

            List<int> foundIndices = LinearSearch(mergedArray, searchValue);

            if (foundIndices.Count > 0)
                Console.WriteLine($"Value {searchValue} found at index/indices: {string.Join(", ", foundIndices)}");
        
            else
        {
            Console.WriteLine($"Value {searchValue} not found.");
            KeyValuePair<int, int> nearestValueInfo = FindNearestValue(mergedArray, searchValue);
            Console.WriteLine($"Nearest value is {nearestValueInfo.Key} at index {nearestValueInfo.Value}");
        }
    }
}

static void BubbleSort(int[] array, bool ascending)
{
    int n = array.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            bool shouldSwap = ascending ? array[j] > array[j + 1] : array[j] < array[j + 1];
            if (shouldSwap)
            {
                int temp = array[j];
                array[j] = array[j + 1];
                array[j + 1] = temp;
            }
        }
    }
}

static void DisplayEveryNthValue(int[] array, int n)
{
    for (int i = 0; i < array.Length; i += n)
    {
        Console.WriteLine($"Value at index {i}: {array[i]}");
    }
}

static List<int> LinearSearch(int[] array, int value)
{
    List<int> indices = new List<int>();
    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == value)
        {
            indices.Add(i);
        }
    }
    return indices;
}

static KeyValuePair<int, int> FindNearestValue(int[] array, int value)
{
    int nearestIndex = 0;
    int smallestDifference = Math.Abs(array[0] - value);

    for (int i = 1; i < array.Length; i++)
    {
        int currentDifference = Math.Abs(array[i] - value);
        if (currentDifference < smallestDifference)
        {
            smallestDifference = currentDifference;
            nearestIndex = i;
        }
    }

    return new KeyValuePair<int, int>(array[nearestIndex], nearestIndex);
}

static int[] MergeArrays(int[] array1, int[] array2)
{
    int[] mergedArray = new int[array1.Length + array2.Length];
    Array.Copy(array1, mergedArray, array1.Length);
    Array.Copy(array2, 0, mergedArray, array1.Length, array2.Length);
    return mergedArray;
}
}
