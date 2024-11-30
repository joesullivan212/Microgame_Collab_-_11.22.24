using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputExample : MonoBehaviour
{
    public MicrogameInputManager microgameInputManager;

    public Vector2 movement;

    void Update()
    {
        if (microgameInputManager.Clicked) 
        {
            Debug.Log("Clicked");
        }
        if (microgameInputManager.Unclicked) 
        {
            Debug.Log("Unclicked");
        }
        if (microgameInputManager.RightClicked)
        {
            Debug.Log("RightClicked");
        }
        if (microgameInputManager.RightUnclicked)
        {
            Debug.Log("RightUnclicked");
        }

        movement = microgameInputManager.MouseMovement;
    }
}
