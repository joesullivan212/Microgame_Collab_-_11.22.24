using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MicrogameInputManager : MonoBehaviour
{
    public Vector2 MouseMovement;
    public Vector2 MouseMovementNormalized;

    public Vector2 ArrowKeysDirection;

    public bool Clicked;
    public bool Unclicked;
    public bool RightClicked;
    public bool RightUnclicked;
    public bool MouseBeingHeld;

    [SerializeField] InputActionReference mouseMovementRef;
    [SerializeField] InputActionReference arrowKeysMovementRef;

    private float Deadzone = 0.1f;

    private void Update()
    {
        MouseMovement = mouseMovementRef.action.ReadValue<Vector2>();

        if(MouseMovement.magnitude > Deadzone) 
        {
            MouseMovementNormalized = MouseMovement.normalized;
        }
        else
        {
            MouseMovementNormalized = new Vector2(0.0f, 0.0f);
        }

        ArrowKeysDirection = arrowKeysMovementRef.action.ReadValue<Vector2>();
    }

    public void PlayerClicked(InputAction.CallbackContext callbackContext) 
    {
        if (callbackContext.performed && callbackContext.canceled == false)
        {
            Clicked = true;

            MouseBeingHeld = true;
        }
    }

    public void PlayerUnclicked(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && callbackContext.canceled == false)
        {
            Unclicked = true;

            MouseBeingHeld = false;
        }
    }

    public void PlayerRightClicked(InputAction.CallbackContext callbackContext) 
    {
        if (callbackContext.performed && callbackContext.canceled == false)
        {
            RightClicked = true;
        }
    }

    public void PlayerRightUnclicked(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && callbackContext.canceled == false)
        {
            RightUnclicked = true;
        }
    }


    public void LateUpdate()
    {
        Clicked = false;
        Unclicked = false;
        RightClicked = false;
        RightUnclicked = false;
    }
}
