using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {


    public EventHandler OnStep;
    
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax;
    
    
    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0) {
            footstepTimer = footstepTimerMax;
            if (player.IsWalking()) {
                SoundManager.Instance.PlayFootstepSound(player.transform.position,0.95f);
            }
        }
    }
    
    
    
}
