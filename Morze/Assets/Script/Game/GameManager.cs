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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject enemyGameObject;
    [SerializeField] private GameObject combatInstallation;


    private List<GameObject> enemyPull = new List<GameObject>();
    private GameObject activeEnemy;
    private bool newObject = true;

    private int firstTime = 2;
    private int lastTime = 5;


    public static String actualWord;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NewEnemy();
        NearestActive();

        Transform child = activeEnemy.transform.Find("Death Code");
        TextMeshProUGUI textCode = child.gameObject.GetComponent<TextMeshProUGUI>();
        actualWord = textCode.text;
    }

    private void Update()
    {
        if (newObject)
            NewEnemy();
    }

    public void NewEnemy()
    {
        GameObject newEnemy = Instantiate(enemyGameObject, transform);
        ActiveText(newEnemy, false);
        enemyPull.Add(newEnemy);
        StartCoroutine(Pause());
    }

    private void ActiveText(GameObject activeObject, bool active)
    {
        Transform child = activeObject.transform.Find("Death Code");

        if (child != null)
            child.gameObject.SetActive(active);           
    }

    private void NearestActive()
    {
        if(enemyPull.Count < 0)
            return;

        activeEnemy = enemyPull.First();

        foreach (var item in enemyPull)
        {
            if (Distance(item) < Distance(activeEnemy))
                activeEnemy = item;
        }

        for (int i = 1; i < enemyPull.Count; i++)
        {
            if (Distance(enemyPull[i]) < Distance(activeEnemy))
                activeEnemy = enemyPull[i];
        }

        ActiveText(activeEnemy, true);
    }

    private double Distance(GameObject distanceObject)
    {
        Vector3 enemyPosition = (Vector3)distanceObject.transform.localPosition;
        Vector3 combatPosition = (Vector3)combatInstallation.transform.localPosition;

        return Math.Sqrt(Math.Pow(combatPosition.x - enemyPosition.x, 2) + Math.Pow(combatPosition.y - enemyPosition.y, 2));
    }

    public void KillEnemy()
    {
        Debug.Log("Kill Enemy");

        enemyPull.Remove(activeEnemy);
        Destroy(activeEnemy);

        NearestActive();

        Transform child = activeEnemy.transform.Find("Death Code");
        TextMeshProUGUI textCode = child.gameObject.GetComponent<TextMeshProUGUI>();
        actualWord = textCode.text;
    }

    public void EndGame()
    {
        Debug.Log("End Game");
    }

    IEnumerator Pause()
    {
        newObject = false;

        float time = Random.Range(firstTime, lastTime);
        yield return new WaitForSeconds(time);

        newObject = true;
    }
}
