using System;
using UnityEngine;
using UnityEngine.UI;

public class bvGamePauseUI : MonoBehaviour
{
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
        });
    }

    
    
    private void Start() {
        Hide();
        KitchenGameManager.Instance.OnPause += KitchenGameManager_OnPause;
        KitchenGameManager.Instance.OnUnPause += KitchenGameManager_OnUnPause;
    }

    private void KitchenGameManager_OnUnPause(object sender, EventArgs e) {
        Hide();
    }

    private void KitchenGameManager_OnPause(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    
}
