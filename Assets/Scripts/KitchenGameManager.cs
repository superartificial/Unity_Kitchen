using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    
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
    private float gamePlayingTimer = 10f;
    
    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
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
        Debug.Log(state);
    }
    
    public bool IsWaitingToStart() => state == State.WaitingToStart;
    public bool IsCountdownToStart() => state == State.CountdownToStart;
    public bool IsGamePlaying() => state == State.GamePlaying;
    public bool IsGameOver() => state == State.GameOver;

    
}
