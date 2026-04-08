using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public  BaseCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] Transform kitchenObjectHoldPoint;

    private bool isWalking = false;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance == null)
        {
            Debug.unityLogger.Log("Player", "Awake()");
        }
        Instance = this;
    }
    
    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs args) {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != this.selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter newBaseCounter) {
        this.selectedCounter = newBaseCounter;
        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = newBaseCounter
        });
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius= 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir,moveDistance );


        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX,moveDistance );

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ,moveDistance );
                if (canMove)  {
                    moveDir = moveDirZ;
                }
                else
                {
                    
                }
            } 
        }    
        if (canMove)
        {
            transform.position += moveDir * (Time.deltaTime * moveSpeed);
        }
        isWalking = moveDir != Vector3.zero;
        transform.forward =  Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed) ;
    }

    public Transform GetKitchenObjectFollowTransform() { return kitchenObjectHoldPoint; }

    public void setKitchenObject(KitchenObject k) {
        kitchenObject = k;
    }
    
    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }
    
    public bool HasKitchenObject() { return kitchenObject != null; }    
}
