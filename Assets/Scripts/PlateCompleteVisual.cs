using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOList;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectso_GameObject in kitchenObjectSOList) {
            kitchenObjectso_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (KitchenObjectSO_GameObject kitchenObjectso_GameObject in kitchenObjectSOList) {
            if (kitchenObjectso_GameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectso_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
