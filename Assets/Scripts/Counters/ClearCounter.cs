using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().setKitchenObjectParent(this);
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    } 
                } else {
                    // player carrying something that is not a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().getKitchenObjectSO())) {
                            // item has been added to plate, remove from player
                            player.GetKitchenObject().DestroySelf();
                        }
                        
                    }
                }
            } else {
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }
}