using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private GameObject combatInstallation;

    private const float angleSpam = 45;
    private const float speed = 2f;

    private Vector3 combatPosition;

    private bool isTrigger = false;

    private void Awake()
    {
        Instance = this;
        combatPosition = (Vector3)combatInstallation.transform.localPosition;
    }

    private void Start()
    {
        Vector3 enemyRotation = (Vector3)transform.eulerAngles;
        Vector3 enemyPosition = (Vector3)transform.localPosition;
        Vector3 enemyScale = (Vector3)transform.localScale;

        enemyRotation.z = Random.Range(- 1 * angleSpam, angleSpam);
        transform.eulerAngles = enemyRotation;

        enemyPosition.y = Camera.main.orthographicSize + enemyScale.y;

        double radians = Math.Abs(enemyRotation.z) * (Math.PI / 180); 
        double x = (combatPosition.y - enemyPosition.y) * Math.Tan(radians);
        x = (enemyRotation.z >= 0) ? x : (-1 * x);
        enemyPosition.x = (float)x;

        transform.localPosition = enemyPosition;
    }

    private void Update()
    {
        if (isTrigger)
            return;

        float step = speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, combatPosition, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
        GameManager.Instance.EndGame();
    }

    public void SetText()
    {

    }

}