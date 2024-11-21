using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerReadout : MonoBehaviour
{
    TextMeshProUGUI tmPro;
    private void Awake()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTimerText(float timeRemaining)
    {
        int timeLeft = (int)timeRemaining;
        tmPro.text = timeLeft.ToString();
    }
}
