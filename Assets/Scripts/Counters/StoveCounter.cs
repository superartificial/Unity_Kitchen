using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChange;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private State state;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;
    
    private void SetState(State newState) {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }

    private void Start() {
        state = State.Idle;
    }
    
    private void Update() {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    
                    break;
                case State.Frying:
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax,
                    });
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.outut, this);
                        burningRecipeSO = GetBurningRecipeSOWithInput(fryingRecipeSO.outut);
                        SetState(State.Fried);
                        burningTimer = 0f;
                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax,
                    });
                    if (burningRecipeSO == null)
                    {
                        Debug.LogError("Recipe is null");
                        break;
                    }
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.outut, this);
                        SetState(State.Burned);
                        OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0,
                        });
                    } 
                    break;
                case State.Burned:
                    break;
                
            }
        }
    }

    public override void Interact(Player player) {
        
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().getKitchenObjectSO()))
                {
                    player.GetKitchenObject().setKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().getKitchenObjectSO());
                    SetState(State.Frying);
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax,
                    });
                    fryingTimer = 0f;
                }
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
                SetState(State.Idle);
                OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0,
                });
            }
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetFryingRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO?.outut;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) 
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }   
        }  
        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
    
}
