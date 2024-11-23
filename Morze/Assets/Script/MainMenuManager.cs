using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttonList;

    private bool isPress = false;

    private int shortPressDuration = 55;
    private int longPressDuration = 100;

    private int currentButton = 0;
    private int currentShortPress;
    private int currentPressDuration;

    private void Awake()
    {
        currentShortPress = shortPressDuration;
        SelectButton();
    }
    
    private void Update()
    {
        if (isPress)
        {
            if (currentShortPress >= 0)
                currentShortPress--;
               
            if (currentShortPress == 0)
                SelectButton();

            currentPressDuration--;
            if (currentPressDuration == 0)
            {
                ChangeScene(currentButton);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPress = true;

            currentShortPress = shortPressDuration;
            currentPressDuration = longPressDuration;

            SelectButton();            
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isPress = false;

            if(currentShortPress > 0)
                currentButton = (currentButton + 1) % buttonList.Count;

            SelectButton();
        }
    }

    private void SelectButton()
    {
        Color activeColor = (currentShortPress == 0 && isPress) ? Color.yellow : Color.red;

        for (int i = 0; i < buttonList.Count; i++)
        {
            Color colorButton = (i == currentButton) ? activeColor : Color.blue;

            var button = buttonList[i];
            var colors = button.colors;
            var disabledColor = colors.disabledColor;
            disabledColor = colorButton;
            colors.disabledColor = disabledColor;
            button.colors = colors;
        }        
    }

    private void ChangeScene(int button)
    {
        switch (button)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Application.Quit();
                break;
        }
    }
}
