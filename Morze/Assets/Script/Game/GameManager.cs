using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem.HID;
using UnityEditor.EditorTools;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private GameObject combatInstallation;
    [SerializeField] private GameObject endGameImage;
    [SerializeField] private GameObject winGameImage;

    private List<GameObject> enemyPull = new List<GameObject>();
    private GameObject activeEnemy;
    private bool newObject = true;
    private bool isActiveEnemy = false;

    public bool inGame = true;
    public bool isActive = true;

    private int numEnemy = 10;

    private int firstTime = 15;
    private int lastTime = 10;

    public bool endGame = false;
    public static String actualWord;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewEnemy();
        endGameImage.SetActive(false);
        winGameImage.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
            Destroy(gameObject);
        else if (SceneManager.GetActiveScene().name == "Settings" && isActive)
        {
            isActive = false;  
            combatInstallation.SetActive(isActive);
        }
        else if (SceneManager.GetActiveScene().name == "Game Scene" && !isActive)
        {
            isActive = true;
            combatInstallation.SetActive(isActive);
        }


        if (!inGame)
            return;

        if (endGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main Menu");
                Destroy(gameObject);
            }
            return;
        }

       
        if (!isActiveEnemy && enemyPull.Count > 0)
        {
            NearestActive();

            ActiveText(activeEnemy, true);

            Transform childText = activeEnemy.transform.Find("Death Code");
            Transform childBody = activeEnemy.transform.Find("Sprite");

            TextMeshProUGUI textCode = childText.gameObject.GetComponent<TextMeshProUGUI>();
            actualWord = textCode.text;

            StvolManager.Instance.SetRotation((Vector3)activeEnemy.transform.localPosition);

            isActiveEnemy = true;
        }

        if (newObject && numEnemy > 0)
            NewEnemy();
    }

    public void StartAgain()
    {
        inGame = true;
        StartCoroutine(Pause());
    }

    public void toSetting()
    {
        inGame = false;
    }

    public void NewEnemy()
    {
        numEnemy--;
        GameObject newEnemy = Instantiate(enemyGameObject, transform);
        ActiveText(newEnemy, false);
        enemyPull.Add(newEnemy);
        StartCoroutine(Pause());
    }

    private void ActiveText(GameObject activeObject, bool active)
    {
        Transform child = activeObject.transform.Find("Death Code");

        if (child != null)
        {
            child.gameObject.SetActive(active);
        }
    }

    private void NearestActive()
    {
        if (enemyPull.Count <= 0)
            return;

        activeEnemy = enemyPull.First();

        foreach (var item in enemyPull)
        {
            if (Distance(item) < Distance(activeEnemy))
                activeEnemy = item;
        }
    }

    private double Distance(GameObject distanceObject)
    {
        Vector3 enemyPosition = (Vector3)distanceObject.transform.localPosition;
        Vector3 combatPosition = (Vector3)combatInstallation.transform.localPosition;

        return Math.Sqrt(Math.Pow(combatPosition.x - enemyPosition.x, 2) + Math.Pow(combatPosition.y - enemyPosition.y, 2));
    }

    public void KillEnemy()
    {
        enemyPull.Remove(activeEnemy);
        Destroy(activeEnemy);

        isActiveEnemy = false;

        if (numEnemy == 0)
        {
            endGame = true;
            winGameImage.SetActive(true);
        }
            
    }

    public void EndGame()
    {
        endGameImage.SetActive(true);
        endGame = true;
    }

    IEnumerator Pause()
    {
        newObject = false;

        float time = Random.Range(firstTime, lastTime);
        yield return new WaitForSeconds(time);

        newObject = true;
    }
}
