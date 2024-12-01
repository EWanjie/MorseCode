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

public class StvolManager : MonoBehaviour
{
    public static StvolManager Instance { get; private set; }

    private GameObject pivotPoint;
    private int angleZ;
    private double finishAngle;

    private bool isActive = false;
    private double lastPosition;
    private Vector3 startPosition;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pivotPoint = new GameObject("Pivot Point");
        pivotPoint.transform.parent = transform.parent;
        pivotPoint.transform.position = transform.position;

        Bounds bounds = GetComponent<Renderer>().bounds;

        startPosition = new Vector3(bounds.center.x, bounds.min.y, 0);
        lastPosition = startPosition.x;

        pivotPoint.transform.position = startPosition;

        transform.parent = pivotPoint.transform;
    }

    void Update()
    {
        if (!isActive)
            return;

        Vector3 enemyRotation = (Vector3)pivotPoint.transform.eulerAngles;
        double curruntAngle = Math.Round(enemyRotation.z);

        pivotPoint.transform.Rotate(0, 0, angleZ);

        if (finishAngle == curruntAngle)
            isActive = false;
    }

    public void SetRotation(Vector3 position)
    {

        angleZ = (startPosition.x > position.x) ? 1 : -1;
        finishAngle = angleZ;

        double tanAlpa = (position.x - startPosition.x) / (position.y - startPosition.y);
        double alpa = Math.Atan(Math.Abs(tanAlpa));
        double angle = alpa * (180 / Math.PI);

        finishAngle = (angleZ >= 0) ? angle : 360 - angle;
        finishAngle = Math.Round(finishAngle);
        isActive = true;
    }
}
