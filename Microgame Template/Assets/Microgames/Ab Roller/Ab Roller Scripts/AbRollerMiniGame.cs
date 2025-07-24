using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class AbRollerMiniGame : MonoBehaviour
{
    [Header("Necessary Components")]
    public MicrogameHandler gameHandler;

    [Header("Settings")]
    public float gameDelay;
    public float gameDuration;
    public float maxReps;

    [Header("Debugging")]
    [SerializeField] private AbRollerGameState state;
    [SerializeField] private int repCounts;
    [SerializeField] private float timer;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLost;
    public UnityEvent OnGamePause;
    public UnityEvent OnGamePreparing;

    private void Awake()
    {
        state = AbRollerGameState.PREPARING;
        repCounts = 0;

        OnGamePreparing?.Invoke();
        gameHandler.PauseTimer();
    }

    void PlayingStateHandler()
    {
        if(repCounts >= maxReps)
        {
            gameHandler.Win();
            gameHandler.CancelTimer();
            state = AbRollerGameState.END;
            OnGameWin?.Invoke();

        }
        else if(timer >= 1.00f)
        {
            gameHandler.Lose();
            gameHandler.CancelTimer();

            state = AbRollerGameState.END;
            OnGameLost?.Invoke();
        }
        else
        {
            timer += Time.deltaTime / gameDuration;
        }
    }
    void PreparingStatehandler()
    {
        if(timer >= 1.00f)
        {
            timer = 0.00f;
            state = AbRollerGameState.PLAYING;

            OnGameStart?.Invoke();
            gameHandler.ResumeTimer();
        }
        else
        {
            timer += Time.deltaTime / gameDelay;
        }
    }
    void StateHandler()
    {
        switch(state) 
        {
            case AbRollerGameState.PREPARING: PreparingStatehandler(); break;
            case AbRollerGameState.PLAYING: PlayingStateHandler(); break;
            default: break;
        
        
        }
    }

    // Update loop just runs the state handler function
    void Update()
    {
        StateHandler();
    }

    public void IncrementRep() => repCounts++;

    public enum AbRollerGameState
    {
        PREPARING,
        PLAYING,
        END
    }

    public int Reps => repCounts;
}
