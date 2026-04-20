using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    
    [SerializeField] private Image timerImage;

    private void Start() {
        timerImage.fillAmount = 0;
    }

    private void Update() {
        if (KitchenGameManager.Instance.IsGamePlaying()) {
            timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        }
    }
}
