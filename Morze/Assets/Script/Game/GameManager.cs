using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject enemyGameObject;

    private Queue<GameObject> enemyPull = new Queue<GameObject>();

    private bool newObject = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //GameObject newCaracrer = Instantiate(enemyGameObject, transform);
        
    }

    private void Update()
    {
        if (newObject)
        {
            GameObject newEnemy = Instantiate(enemyGameObject, transform);
            enemyPull.Enqueue(newEnemy);
            StartCoroutine(Pause());
        }
    }

    public void KillEnemy()
    {
        Debug.Log("Kill Enemy");
    }

    public void EndGame()
    {
        Debug.Log("End Game");

        GameObject destroyEnemy = enemyPull.Dequeue();

        if (destroyEnemy != null)
        {
            Destroy(destroyEnemy);
        }             
    }

    IEnumerator Pause()
    {
        newObject = false;

        float time = Random.Range(2, 5);
        yield return new WaitForSeconds(time);

        newObject = true;
    }
}
