using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class TextControler : MonoBehaviour
{
    public static TextControler Instance { get; private set; }

    [SerializeField] private TextAsset morzeText;
    [SerializeField] private GameObject morzeDecoderText;

    Dictionary<char, string> morzeDictionary = new Dictionary<char, string>();

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI scoreDecoderText;

    private string morzeCode = "";
    private string morzeDecoderCode = "";

    private string currentSymbol = "";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ReadDataFromFile();

        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        scoreDecoderText = morzeDecoderText.gameObject.GetComponent<TextMeshProUGUI>();

        scoreText.text = morzeCode;
        scoreDecoderText.text = morzeDecoderCode;
    }

    public void AddSymbol(char symbol)
    {
        morzeCode += symbol;
        currentSymbol += symbol;
        scoreText.text = morzeCode;
    }


    public void Reset()
    {
        if (morzeDecoderCode == GameManager.actualWord)
        {
            GameManager.Instance.KillEnemy();
        }

        morzeCode = "";
        morzeDecoderCode = "";

        scoreText.text = morzeCode;
        scoreDecoderText.text = morzeDecoderCode;
    }

    public void Decoder()
    {
        morzeCode += ' ';

        if (morzeDictionary['o'] == currentSymbol)
        {
            Reset();
            AudioMixerManager.lastScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.toSetting();
            SceneManager.LoadScene("Settings");
            return;
        }


        bool isFind = false;

        foreach (KeyValuePair<char, string> letter in morzeDictionary)
        {
            if(letter.Value == currentSymbol)
            {
                morzeDecoderCode += letter.Key;
                isFind = true;
                continue;
            }
        }

        if(!isFind)
        {
            morzeDecoderCode += currentSymbol;
        }

        currentSymbol = "";
        scoreDecoderText.text = morzeDecoderCode;
    }

    private void ReadDataFromFile()
    {
        if(morzeText == null)
        {
            Debug.LogError("TextAsset не назначен!");
            return;
        }

        string fileContent = morzeText.text;

        using (StringReader reader = new StringReader(fileContent))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[] { ' ' }, 2);
                if (parts.Length == 2)
                {
                    char symbol = parts[0][0];
                    string str = parts[1];

                    morzeDictionary[symbol] = str;
                }
                else
                {
                    Debug.LogError("Неверный формат строки: " + line);
                }
            }
        }
    }
}
