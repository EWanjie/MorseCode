using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private List<ButtonTupe> buttonList;
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

    [System.Serializable]
    public class ButtonTupe
    {
        public Button button;

        public Sprite normal;
        public Sprite pressed;
        public Sprite selected;
    }
}
