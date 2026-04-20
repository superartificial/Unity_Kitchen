using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnPause;
    public event EventHandler OnUnPause;
    
    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private State state {
        get => _state;
        set {
            _state = value;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 30f;
    private bool isGamePaused = false;
    
    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    public void TogglePauseGame() {
        isGamePaused =  !isGamePaused;
        Time.timeScale = (isGamePaused) ? 0f : 1f;
        if (isGamePaused) {
            OnPause?.Invoke(this, EventArgs.Empty);
        }
        else {
            OnUnPause?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                } 
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                } 
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;
                } 
                break;
            case State.GameOver:
                break;
        }
    }
    
    public bool IsWaitingToStart() => state == State.WaitingToStart;
    public bool IsCountdownToStart() => state == State.CountdownToStart;
    public bool IsGamePlaying() => state == State.GamePlaying;
    public bool IsGameOver() => state == State.GameOver;

    public float GetCountdownToStartTimer() => countdownToStartTimer;
    
    public float GetGamePlayingTimerNormalized() => 1 - (gamePlayingTimer / gamePlayingTimerMax);
    
}
