using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance { get; private set; }

    [SerializeField] private Sprite onLight;
    [SerializeField] private Sprite offLight;

    private void Awake()
    {
        Instance = this;
    }

    public void OnOffLight(bool isLight)
    {
        GetComponent<SpriteRenderer>().sprite = (isLight) ? onLight : offLight;
    }
}
