using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControler : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        setText();
    }

    private void setText()
    {
        scoreText.text = "Sisi";
    }
}
