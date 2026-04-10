using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    
    private List<GameObject> platesVisualGameObjectList;

    private void Awake() {
        platesVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlateCounterOnPlateSpawned;
        platesCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
    }

    private void PlateCounterOnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = platesVisualGameObjectList[platesVisualGameObjectList.Count - 1];
        platesVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounterOnPlateSpawned(object sender, EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * platesVisualGameObjectList.Count, 0);
        platesVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
    
}
