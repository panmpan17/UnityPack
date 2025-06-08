using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    //     int[] numbers = new int[] { 1, 7, 4, 2, 5, 6, 9}; // Length 7
    //     // int[] numbers = new int[] { 2, 5, 6, 9 }; // Length 7

    //     // 1, 7, 4 => 1, 4, 7
    //     // 1 => 1
    //     // 7, 4 => 4, 7

    //     // 2, 5, 6, 9 => 2, 5, 6, 9
    //     // 2, 5 => 2, 5
    //     // 6, 9 => 6, 9

    //     InsertSort(ref numbers);
    //     for (int i = 0; i < numbers.Length; i++) Debug.Log(numbers[i]);

    // }


    void Start()
    {
        System.String inputData = "ABABAC";
        char[] charArray = inputData.ToCharArray();
        Dictionary<char, int> characterCounts = new Dictionary<char, int>();

        for (int i = 0; i < charArray.Length; i++) {
            char c = charArray[i];
            if (characterCounts.TryGetValue(c, out int count)) {
                characterCounts[c] = count + 1;
            }
            else characterCounts.Add(c, 1);
        }
        var builder = new System.Text.StringBuilder();
        bool noRepeat = true;
        foreach (var pair in characterCounts) {
            if (pair.Value > 1) {
                noRepeat = false;
                builder.Append(pair.Key);
                builder.Append(" appeared ");
                builder.Append(pair.Value);
                builder.Append(" times. ");
            }
        }
        if (noRepeat)
            Debug.Log("no reoccurrence");
        else 
            Debug.Log(builder.ToString());

    }

    void InsertSort(ref int[] array) {
        if (array.Length == 1)
            return;
        if (array.Length == 2)
        {
            if (array[0] > array[1])
            {
                int temp = array[1];
                array[1] = array[0];
                array[0] = temp;
            }              
            return;
        }  

        int middleIndex = array.Length / 2; // 3
        int[] firstHalf = new int[middleIndex];
        int[] secondHalf = new int[array.Length - middleIndex];

        for (int i = 0; i < array.Length; i++) {
            if (i < middleIndex) firstHalf[i] = array[i];
                else secondHalf[i - middleIndex] = array[i];
        }

        InsertSort(ref firstHalf);
        InsertSort(ref secondHalf);

        int firstIndex = 0, secondIndex = 0;
        for (int i = 0; i < array.Length; i++) {
            if (firstIndex >= firstHalf.Length) {
                array[i] = secondHalf[secondIndex++];
                continue;
            }
            if (secondIndex >= secondHalf.Length) {
                array[i] = firstHalf[firstIndex++];
                continue;
            }

            if (firstHalf[firstIndex] <= secondHalf[secondIndex]) {
                array[i] = firstHalf[firstIndex++];
                continue;
            }
            else {
                array[i] = secondHalf[secondIndex++];
                continue;
            }
        }
    }

}
