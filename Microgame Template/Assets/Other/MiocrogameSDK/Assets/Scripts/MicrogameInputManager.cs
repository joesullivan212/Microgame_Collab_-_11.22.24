using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MicrogameInputManager : MonoBehaviour
{
    [Header("Input")]
    public Vector2 MouseMovement;
    public Vector2 MouseMovementNormalized;
    public Vector2 ArrowKeysDirection;
    public Vector2 MouseWorldPoint;
    public Vector2 MouseScreenPosition;

    public bool Clicked;
    public bool Unclicked;
    public bool RightClicked;
    public bool RightUnclicked;
    public bool MouseBeingHeld;


    [Header("Refs")]
    [SerializeField] InputActionReference mouseMovementRef;
    [SerializeField] InputActionReference arrowKeysMovementRef;
    private float Deadzone = 0.1f;
    public PlayerInput playerInput;
    public Camera camera;
    public GameObject VirtualMouseSystem;
    public RectTransform VirtualMouseTransform;

    private void Start()
    {
       // if(camera == null) 
       // {
       //     if (Camera.current.GetComponent<Camera>() != null)
       //     {  camera = Camera.current.GetComponent<Camera>(); }
       // }

        if (camera == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() != null) { 
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); }
        }

        if (camera == null) 
        {
            Debug.LogError("NO CAMERA FOUND");
        }
    }

    private void Update()
    {
        //Mouse
        if (playerInput.currentControlScheme == "Keyboard&Mouse") 
        {
            VirtualMouseSystem.SetActive(false);

            MouseScreenPosition = Input.mousePosition;
            MouseWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if(playerInput.currentControlScheme == "Gamepad") 
        {
            VirtualMouseSystem.SetActive(true);

            MouseScreenPosition = VirtualMouseTransform.position;
            MouseWorldPoint = camera.ScreenToWorldPoint(VirtualMouseTransform.position);
        }

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
