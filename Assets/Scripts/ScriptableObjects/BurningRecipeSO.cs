using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{

    public KitchenObjectSO input;
    public KitchenObjectSO outut;
    public int burningTimerMax;
}
