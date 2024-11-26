using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AskManager : MonoBehaviour
{
    public static AskManager Instance { get; private set; }

    [SerializeField] private List<ButtonTupe> buttonList;
    [SerializeField] GameObject askBox;

    public static bool isActive = false;
    private bool isPress = false;

    private bool isFirst = true;
    private int timerDuration = 5;
    private int timer = 0;

    private int shortPressDuration = 55;
    private int longPressDuration = 100;

    private int currentButton = 0;
    private int currentShortPress;
    private int currentPressDuration;


    private void Awake()
    {
        Instance = this;

        askBox.SetActive(isActive);

        currentShortPress = shortPressDuration;
        currentPressDuration = longPressDuration;

        SelectButton();
    }

    public void toActive(bool active)
    {
        isActive = active;
        askBox.SetActive(isActive);
    }


    private void Update()
    {
        if(!isActive)
            return;

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
        Sprite currontSprite;
        currontSprite = (currentShortPress == 0 && isPress) ? buttonList[currentButton].pressed : buttonList[currentButton].selected;

        for (int i = 0; i < buttonList.Count; i++)
        {
            Sprite sprite = (i == currentButton) ? currontSprite : buttonList[i].normal;
            buttonList[i].button.image.sprite = sprite;
        }
    }

    private void ChangeScene()
    {
        switch (currentButton)
        {
            case 0:
                isActive = false;
                askBox.SetActive(isActive);
                SettingManager.isActive = !isActive;
                break;
            case 1:
                SceneManager.LoadScene("Main Menu");
                break;
        }
    }


    [System.Serializable]
    public class ButtonTupe
    {
        public Button button;

        public Sprite normal;
        public Sprite pressed;
        public Sprite selected;
    }
}
