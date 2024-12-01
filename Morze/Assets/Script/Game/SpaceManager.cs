using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    private bool isSpace = false;
    private bool isInput = false;

    private bool isFirst = true;
    private int timerDuration = 5;
    private int timer = 0;

    private const int pointSpeed = 25; // Скорость нажатия на клавишу
    private const int dashSpeed = 3 * pointSpeed;
    private const int symbolSpeed = 5 * pointSpeed;
    private const int whitespaceSpeed = 15 * pointSpeed;

    private int curentDashSpeed;
    private int curentSymbolSpeed;
    private int curentWhitespaceSpeed;

    private void Start()
    {
        curentDashSpeed = dashSpeed;
        curentSymbolSpeed = symbolSpeed;
        curentWhitespaceSpeed = whitespaceSpeed;
    }

    private void Update()
    {
        if (isFirst)
            timer++;

        if (!isInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                isInput = true;
            else
                return;  
        }

        if (isSpace)
        {
            if (curentDashSpeed > 0)
                curentDashSpeed--;
        }
        else
        {
            if(curentSymbolSpeed >= 0)
                curentSymbolSpeed--;
            if (curentSymbolSpeed == 0)
                TextControler.Instance.Decoder();

            if (curentWhitespaceSpeed >= 0)
                curentWhitespaceSpeed--;
            if (curentWhitespaceSpeed == 0)
                TextControler.Instance.Reset();
        }

        Space();
    }

    private void Space()
    {
        bool currentSpace = isSpace;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isFirst && timer < timerDuration)
                return;

            isFirst = false;
            isSpace = true;

            if (curentDashSpeed == dashSpeed - 1)
                return;

            curentDashSpeed = dashSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isFirst)
            {
                isFirst = false;
                return;
            }

            isSpace = false;

            char symbol = (curentDashSpeed == 0) ? '-' : '.';
            TextControler.Instance.AddSymbol(symbol);

            curentSymbolSpeed = symbolSpeed;
            curentWhitespaceSpeed = whitespaceSpeed;
        }

        if (currentSpace != isSpace)
        {
            AudioManager.Instance.PlayingRadio(isSpace);
            LightManager.Instance.OnOffLight(isSpace);
            currentSpace = isSpace;
        }        
    }
}
