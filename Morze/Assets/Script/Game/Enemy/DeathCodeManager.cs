using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using TMPro;

public class DeathCodeManager : MonoBehaviour
{
    private TextMeshProUGUI textCode;

    public static string deachCode;

    private const int minWordLength = 1;
    private const int maxWordLength = 4;

    private const int asciiA = 65;
    private const int asciiZ = 90;

    private void Awake()
    {
        deachCode = "";

        int wordLength = Random.Range(minWordLength, maxWordLength);
        for (int i = 0; i < wordLength; i++)
        {
            int asciiChar = Random.Range(asciiA, asciiZ);
            char letter = (char)asciiChar;

            deachCode += letter;
        }

        textCode = gameObject.GetComponent<TextMeshProUGUI>();
        textCode.text = deachCode;

    }
}
