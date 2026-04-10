using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject IconTemplate;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (var kitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList()) {
            
        }
    }
}
