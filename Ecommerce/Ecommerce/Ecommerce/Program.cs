using System;
using System.Collections.Generic;
class Triplet
{
    public static bool CheckTriplet(int[] arr, int n)
    {
        HashSet<int> squares = new HashSet<int>();
        foreach (int num in arr) { squares.Add(num * num); }

        //Step 2: Iterate through all pairs in the array

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                int a2b2 = arr[i] * arr[i] + arr[j] * arr[j];
                if (squares.Contains(a2b2))
                { return true; }
            }
        }
        return false;
    }
    // Step 1: Create a HashSet to store squares of all elements

    static void Main()
    {
        int[] arr = { 3, 2, 4, 6, 5 }; int n = arr.Length;
        bool result = CheckTriplet(arr, n);
        Console.WriteLine(result ? "Yes" : "No");
    }
    
} 