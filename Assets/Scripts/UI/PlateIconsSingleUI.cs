using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour {

    [SerializeField] private Image image;

    public void SetKitchenObjectOS(KitchenObjectSO kitchenObjectSO) {
        image.sprite = kitchenObjectSO.sprite;
    }

}
