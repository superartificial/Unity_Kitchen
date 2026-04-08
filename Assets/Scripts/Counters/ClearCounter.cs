using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player) {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().setKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }
    

    
    
}
