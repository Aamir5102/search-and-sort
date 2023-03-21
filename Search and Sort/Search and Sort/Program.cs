using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] files = { "Road_1_256.txt", "Road_2_256.txt", "Road_3_256.txt", "Road_1_2048.txt", "Road_2_2048.txt", "Road_3_2048.txt", "Merged_256.txt", "Merged_2048.txt" };
        int[][] dataArrays = new int[files.Length][];

        for (int i = 0; i < 6; i++)
        {
            string[] lines = File.ReadAllLines(files[i]);
            dataArrays[i] = Array.ConvertAll(lines, int.Parse);
        }

        int[] merged256 = MergeArrays(dataArrays[0], dataArrays[2]);
        int[] merged2048 = MergeArrays(dataArrays[3], dataArrays[5]);

        dataArrays[6] = merged256;
        dataArrays[7] = merged2048;

        Console.WriteLine("Select a file:");
        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {files[i]}");
        }

        int fileIndex = int.Parse(Console.ReadLine()) - 1;
        int[] selectedArray = dataArrays[fileIndex];

        Console.WriteLine("Enter a value to search for:");
        int searchValue = int.Parse(Console.ReadLine());

        // Process the selected file
        Console.WriteLine($"Processing file {files[fileIndex]}");

        int displayInterval = (fileIndex < 3) ? 10 : (fileIndex < 6) ? 50 : (fileIndex == 6) ? 10 : 50;

        BubbleSort(selectedArray, true); // Sort in ascending order
        Console.WriteLine("Sorted in ascending order:");
        DisplayEveryNthValue(selectedArray, displayInterval);

        BubbleSort(selectedArray, false); // Sort in descending order
        Console.WriteLine("Sorted in descending order:");
        DisplayEveryNthValue(selectedArray, displayInterval);

        List<int> foundIndices = LinearSearch(selectedArray, searchValue);

        if (foundIndices.Count > 0)
        {
            Console.WriteLine($"Value {searchValue} found at index/indices: {string.Join(", ", foundIndices)}");
        }
        else
        {
            Console.WriteLine($"Value {searchValue} not found.");
            KeyValuePair<int, int> nearestValueInfo = FindNearestValue(selectedArray, searchValue);
            Console.WriteLine($"Nearest value is {nearestValueInfo.Key} at index {nearestValueInfo.Value}");
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

