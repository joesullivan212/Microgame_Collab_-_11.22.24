using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Timer : MonoBehaviour
{
    [Header("DEBUG only, set duration in data")]
    public float remainingTime;
    public float maxTime;

    private bool isCounting = true;
    public event Action OnTimeUp;
    public UnityEvent<float> OnUpdateCurrentTime;

    void Update()
    {
        if (!isCounting)
            return;
        
        remainingTime -= Time.deltaTime;
        OnUpdateCurrentTime?.Invoke(remainingTime);

        if (remainingTime <= 0)
            EndTimer(); 
    }

    public void SetDuration(float duration)
    {
        maxTime = duration;
    }

    public void PauseTimer()
    {
        isCounting = false;
    }

    public void ResumeTimer()
    {
        isCounting = true;
    }
    public void CancelTimer()
    {
        isCounting = false;
    }

    public void StartTimer()
    {
        isCounting = true;
        remainingTime = maxTime;
    }

    private void EndTimer()
    {
        isCounting = false;
        OnTimeUp?.Invoke();
    }

    private void OnDisable()
    {
        OnTimeUp = null;
    }
}


