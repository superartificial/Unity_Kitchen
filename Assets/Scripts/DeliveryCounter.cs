using UnityEngine;

public class DeliveryCounter : BaseCounter
{
   
   // Singleton pattern
   public static DeliveryCounter Instance { get; private set; }

   private void Awake() {
      Instance = this;
   }
   
   public override void Interact(Player player) {
      if (player.HasKitchenObject()) {
         if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) {
            // Singleton pattern
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.GetKitchenObject().DestroySelf();
         }
      }
   }
}
