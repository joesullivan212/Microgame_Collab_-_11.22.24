using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class MicrogameHandler : MonoBehaviour
{
    [Header("Create a D_Microgame using SriptableObjects/MicroGame")]
    [SerializeField] D_Microgame microgameData;

    [SerializeField] Timer timer;

 

    public static event Action OnWin;
    public static event Action OnLose;

    private void Awake()
    { 
        timer.SetDuration(microgameData.TimerDuration);
        timer.StartTimer();
        timer.OnTimeUp += TimeUp;
    }

    private void TimeUp()
    {
        if (microgameData.OutcomeOnTimeUp == Outcome.Win)
            Win();
        else 
            Lose();
    }
    public void Win()
    {
        Debug.Log("Microgame WON!");
        timer.CancelTimer();
        OnWin?.Invoke();
    }
    public void Lose()
    {
        Debug.Log("Microgame LOST!");
        timer.CancelTimer();
        OnLose?.Invoke();
    }

    private void OnDisable()
    {
        timer.OnTimeUp -= TimeUp;
    }

    
}
