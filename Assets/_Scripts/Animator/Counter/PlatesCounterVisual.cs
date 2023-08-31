using System;
using System.Collections.Generic;
using _Scripts.Counter;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatesCounterVisual : MonoBehaviour
{
    [FormerlySerializedAs("CounterTopPoint")] [SerializeField] private Transform counterTopPoint;
    [FormerlySerializedAs("PlateVisualPrefab")] [SerializeField] private Transform plateVisualPrefab;
    [FormerlySerializedAs("PlatesCounter")] [SerializeField] private PlatesCounter platesCounter;
    private List<GameObject> plateVisualGameList;

    private void Awake()
    {
        plateVisualGameList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounterOnOnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounterOnOnPlateRemoved;
    }

    private void PlatesCounterOnOnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateObject = plateVisualGameList[plateVisualGameList.Count - 1];
        Destroy(plateObject);
        plateVisualGameList.Remove(plateObject);
    }

    private void PlatesCounterOnOnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameList.Count, 0);
        plateVisualGameList.Add(plateVisualTransform.gameObject);
    }
}
