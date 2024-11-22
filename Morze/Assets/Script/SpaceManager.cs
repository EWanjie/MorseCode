using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    private bool isSpace = false;
    private bool isMusic = true;
    private bool isInput = false;

    private const int pointSpeed = 25; // Скорость нажатия на клавишу
    private const int dashSpeed = 3 * pointSpeed;
    private const int symbolSpeed = 4 * pointSpeed;
    private const int whitespaceSpeed = 10 * pointSpeed;

    private int curentDashSpeed;
    private int curentSymbolSpeed;
    private int curentWhitespaceSpeed;

    private void Start()
    {
        curentSymbolSpeed = symbolSpeed;
        curentWhitespaceSpeed = whitespaceSpeed;
    }

    private void Update()
    {
        Music();

        if (!isInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                isInput = true;
            else
                return;  
        }

        Space();

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
                TextControler.Instance.AddSymbol(' ');
        }   
    }

    private void Space()
    {
        bool currentSpace = isSpace;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpace = true;
            curentDashSpeed = dashSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
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





    private void Music() // Отключение музыки
    {
        if (Input.GetKeyDown(KeyCode.M))   // Отключение музыки. Удалить в последствии из-за неудобства
        {                                   // Включение - выключение музыки. Исправить с кнопки клавиатуры на кнопку
            isMusic = !isMusic;
            AudioManager.Instance.PlayingBackground(isMusic);
            //Изменить иконку звука
        }
    }

}
