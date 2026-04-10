using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    private IKitchenObjectParent kitchenObjectParent;
    
    public KitchenObjectSO getKitchenObjectSO() { return kitchenObjectSO; }
    
    public IKitchenObjectParent getKitchenObjectParent() { return kitchenObjectParent; }

    public void setKitchenObjectParent(IKitchenObjectParent newParent) {    
        if (kitchenObjectParent != null)
            kitchenObjectParent.ClearKitchenObject();
        kitchenObjectParent = newParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has KO");
        }
        kitchenObjectParent.setKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    } 
    
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.setKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        } else {
            plateKitchenObject = null;
            return false;
        }
        
    }
    
    
}
