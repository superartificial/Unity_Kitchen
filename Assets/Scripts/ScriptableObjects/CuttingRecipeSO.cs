using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {

    public KitchenObjectSO input;
    public KitchenObjectSO outut;
    public float cuttingProgressMax;

}
