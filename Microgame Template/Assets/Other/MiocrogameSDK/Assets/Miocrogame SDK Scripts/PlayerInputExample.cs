using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class PlayerInputExample : MonoBehaviour
{
    public MicrogameInputManager microgameInputManager;

    public Vector2 movement;

    public bool SendDebugLogsOfPlayerInput;

    void Update()
    {
        if (microgameInputManager.Clicked) 
        {
            LogInput("Clicked");
        }
        if (microgameInputManager.Unclicked) 
        {
            LogInput("Unclicked");
        }
        if (microgameInputManager.RightClicked)
        {
            LogInput("RightClicked");
        }
        if (microgameInputManager.RightUnclicked)
        {
            LogInput("RightUnclicked");
        }

        movement = microgameInputManager.MouseMovement;
    }





    public void LogInput(string message) 
    {
        if (SendDebugLogsOfPlayerInput)
        {
            Debug.Log(message);
        }
    }
}
