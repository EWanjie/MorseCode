using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class EnemySpriteManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> enemyListAnination;

    private void Awake()
    {
        int randomSprite = Random.Range(0, enemyListAnination.Count);
        GetComponent<SpriteRenderer>().sprite = enemyListAnination[randomSprite];

        gameObject.AddComponent<PolygonCollider2D>();
    }
}
