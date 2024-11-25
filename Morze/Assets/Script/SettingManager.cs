using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    //[SerializeField] private List<Button> buttonList;
    [SerializeField] private List<ButtonTupe> buttonList;

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
        
        SelectButton();
    }

    private void Start()
    {
        string music = (AudioMixerManager.isMusic) ? "Music: on" : "Music: off";
        buttonList[0].button.GetComponentInChildren<TextMeshProUGUI>().text = music;

        string sound = (AudioMixerManager.isSound) ? "Sounds: on" : "Sound: off";
        buttonList[1].button.GetComponentInChildren<TextMeshProUGUI>().text = sound;
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
                ChangeScene(currentButton);
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

    private void ChangeScene(int index)
    {
        switch (index)
        {
            case 0:
                AudioMixerManager.Instance.SetMusic();
                string music = (AudioMixerManager.isMusic) ? "Music: on" : "Music: off";
                buttonList[index].button.GetComponentInChildren<TextMeshProUGUI>().text = music;
                break;
            case 1:
                AudioMixerManager.Instance.SetSound();
                string sound = (AudioMixerManager.isSound) ? "Sounds: on" : "Sound: off";
                buttonList[index].button.GetComponentInChildren<TextMeshProUGUI>().text = sound;
                break;
            case 2:
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

        //public Sprite onNormal;
        //public Sprite onPressed;
        //public Sprite onSelected;

        //public Sprite offNormal;
        //public Sprite offPressed;
        //public Sprite offSelected;
    }
}

