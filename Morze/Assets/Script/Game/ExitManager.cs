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

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance { get; private set; }

    public List<GameObject> enemyPull = new List<GameObject>();
    public int numEnemy = 10;

    void Awake()
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
}