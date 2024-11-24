using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttonList;

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
        buttonList[0].GetComponentInChildren<TextMeshProUGUI>().text = music;

        string sound = (AudioMixerManager.isSound) ? "Sounds: on" : "Sound: off";
        buttonList[1].GetComponentInChildren<TextMeshProUGUI>().text = sound;
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

    private void ChangeScene(int index)
    {
        switch (index)
        {
            case 0:
                AudioMixerManager.Instance.SetMusic();
                string music = (AudioMixerManager.isMusic) ? "Music: on" : "Music: off";
                buttonList[index].GetComponentInChildren<TextMeshProUGUI>().text = music;
                break;
            case 1:
                AudioMixerManager.Instance.SetSound();
                string sound = (AudioMixerManager.isSound) ? "Sounds: on" : "Sound: off";
                buttonList[index].GetComponentInChildren<TextMeshProUGUI>().text = sound;
                break;
            case 2:
                SceneManager.LoadScene("Main Menu");
                break;
        }
    }
}

