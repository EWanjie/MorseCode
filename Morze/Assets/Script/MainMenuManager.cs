using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttonList;
    [SerializeField] private GameObject aboutUs;

    private bool isFirst = true;
    private int timerDuration = 5;
    private int timer = 0;

    private bool isPress = false;
    private bool isAboutUs = false;

    private int shortPressDuration = 55;
    private int longPressDuration = 100;

    private int currentButton = 0;
    private int currentShortPress;
    private int currentPressDuration;

    private void Awake()
    {
        currentShortPress = shortPressDuration;
        currentPressDuration = longPressDuration;

        aboutUs.SetActive(isAboutUs);
        SelectButton();
    }
    
    private void Update()
    {
        if (isFirst)
            timer++;

        if (isPress)
        {
            if (currentShortPress >= 0)
                currentShortPress--;
               
            if (currentShortPress == 0)
                SelectButton();

            currentPressDuration--;
            if (currentPressDuration == 0)
            {
                ChangeScene();
            }
        }

        Space();
    }

    private void Space()
    {
        bool currentSpace = isPress;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isFirst && timer < timerDuration)
                return;

            isFirst = false;
            isPress = true;

            currentShortPress = shortPressDuration;
            currentPressDuration = longPressDuration;

            SelectButton();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isFirst)
            {
                isFirst = false;
                return;
            }

            isPress = false;

            if (isAboutUs)
                return;

            if (currentShortPress > 0)
                currentButton = (currentButton + 1) % buttonList.Count;

            SelectButton();
        }

        if (currentSpace != isPress)
        {
            AudioManager.Instance.PlayingRadio(isPress);
            currentSpace = isPress;
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

    private void ChangeScene()
    {
        switch (currentButton)
        {
            case 0:
                SceneManager.LoadScene("Game Scene");
                break;
            case 1:
                SceneManager.LoadScene("Settings");
                break;
            case 2:
                isAboutUs = !isAboutUs;
                aboutUs.SetActive(isAboutUs);
                break;
            case 3:
                Application.Quit();
                break;
        }
    }
}
