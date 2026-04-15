using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound( audioClipRefsSO.trash, trashCounter!.transform.position);  
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound( audioClipRefsSO.objectDrop,baseCounter!.transform.position);
    }

    private void Player_OnPickedUpSomething(object sender, EventArgs e) {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound( audioClipRefsSO.chop, cuttingCounter!.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position );
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
    
    private void PlaySound(AudioClip[] clipArray, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(clipArray[Random.Range(0,clipArray.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f) {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
    
}
