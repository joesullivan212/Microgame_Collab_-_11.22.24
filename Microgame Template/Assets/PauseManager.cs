using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool IsPaused = false;

    void Update()
    {
        if (IsPaused) 
        { 
           Time.timeScale = 0.0f;
        }    
        else 
        { 
            Time.timeScale = 1.0f;
        }
    }

    public void Pause() 
    {
        IsPaused = true;
    }

    public void Unpause() 
    {
        IsPaused = false;
    }
}
