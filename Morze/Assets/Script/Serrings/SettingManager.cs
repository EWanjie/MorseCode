using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    [SerializeField] private List<ButtonTupe> buttonList;
    [SerializeField] GameObject newBackground;

    public static bool isActive;

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
        currentShortPress = shortPressDuration;
        currentPressDuration = longPressDuration;

        isActive = true;
        AskManager.Instance.toActive(false);

        buttonList[0].isActive = AudioMixerManager.isMusic;
        buttonList[1].isActive = AudioMixerManager.isSound;

        SelectButton();
    }

    private void Start()
    {
        if(AudioMixerManager.lastScene == "Main Menu")
        {
            buttonList[3].button.gameObject.SetActive(false);
            buttonList.RemoveAt(buttonList.Count - 1);

            newBackground.gameObject.SetActive(false);
        }
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

        if(buttonList[currentButton].isActive)
            currontSprite = (currentShortPress == 0 && isPress) ? buttonList[currentButton].onPressed : buttonList[currentButton].onSelected;
        else
            currontSprite = (currentShortPress == 0 && isPress) ? buttonList[currentButton].offPressed : buttonList[currentButton].offSelected;


        for (int i = 0; i < buttonList.Count; i++)
        {
            Sprite sprite;

            sprite = (buttonList[i].isActive) ? buttonList[i].onNormal : buttonList[i].offNormal;

            buttonList[i].button.image.sprite = (i == currentButton) ? currontSprite : sprite;
        }
    }

    private void ChangeScene()
    {
        switch (currentButton)
        {
            case 0:
                AudioMixerManager.Instance.SetMusic();
                buttonList[0].isActive = AudioMixerManager.isMusic;
                SelectButton();
                break;
            case 1:
                AudioMixerManager.Instance.SetSound();
                buttonList[1].isActive = AudioMixerManager.isSound;
                SelectButton();
                break;
            case 2:
                if (AudioMixerManager.lastScene == "Main Menu")
                {
                    SceneManager.LoadScene("Main Menu");
                }
                else
                {
                    isActive = false;
                    AskManager.Instance.toActive(true);
                }
                    
                break;
            case 3:
                SceneManager.LoadScene("Game Scene");
                GameManager.Instance.StartAgain();
                break;
        }
    }

    [System.Serializable]
    public class ButtonTupe
    {
        public Button button;

        [System.NonSerialized] public bool isActive = true;

        public Sprite onNormal;
        public Sprite onPressed;
        public Sprite onSelected;

        public Sprite offNormal;
        public Sprite offPressed;
        public Sprite offSelected;
    }
}

