using UnityEngine;

public interface IKitchenObjectParent {


    public Transform GetKitchenObjectFollowTransform();

    public void setKitchenObject(KitchenObject k);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
