using System;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
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

    private void PlaySound(AudioClip clip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(clip, position, volumeMultiplier * volume);
    }
    
    private void PlaySound(AudioClip[] clipArray, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(clipArray[Random.Range(0,clipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f) {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void ChangeVolume() {
        volume += .1f;
        if(volume > 1f) volume = 0f;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save(); // generally not needed but will make sure it's saved if there is something like a crash before auto saved
    }

    public float GetVolume() => volume;
    
}
