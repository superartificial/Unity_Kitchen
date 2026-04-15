using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    
    [SerializeField] private RecipeListSO recipeListSo;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    // Singleton pattern
    public static DeliveryManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSo.recipeSOList[Random.Range(0, recipeListSo.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int index = 0; index < waitingRecipeSOList.Count; index++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[index];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    bool ingredientFound = false;
                    foreach (var plateKitchObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (plateKitchObjectSO == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // recipe ingredient was not found on plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // player delivered correct recipe
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    Debug.Log("player delivered correct recipe");
                    waitingRecipeSOList.RemoveAt(index);
                    return;
                }
            }
        }
        // Player NOT delivered correct recipe
        Debug.Log("player NOT delivered correct recipe");
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
        
    }
    
    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
}
