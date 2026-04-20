using System;
using UnityEngine;

// Important to clean up static fields
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake() {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
