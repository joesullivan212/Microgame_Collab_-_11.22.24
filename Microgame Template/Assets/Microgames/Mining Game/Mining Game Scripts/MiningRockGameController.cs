using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningRockGameController : MonoBehaviour
{
    public MicrogameInputManager microgameInputManager;
    public MicrogameHandler microgameHandler;
    public GameObject CameraOrbit;
    public float RotationSpeed = 3.0f;
    public float RatiotoRotation = 0.3f;
    public LayerMask layerMask; // Set this in the inspector to filter raycast layers

    public GameObject Explosion;

    [Header("Debug")]
    public float PixelRangeToRotation;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            RaycastFromMouse();
        }

        CheckIfCameraShouldRotate();
    }

    void CheckIfCameraShouldRotate() 
    {
        PixelRangeToRotation = Screen.width / RatiotoRotation;

        //Rotate left
        if (microgameInputManager.MouseScreenPosition.x < PixelRangeToRotation) 
        {
            RotateLeft();
        }
        if(microgameInputManager.MouseScreenPosition.x > Screen.width - PixelRangeToRotation) 
        {
            RotateRight();
        }
        
    }

    void RotateLeft() 
    {
        CameraOrbit.transform.Rotate(new Vector3(0.0f, -RotationSpeed * Time.deltaTime, 0.0f));
    }

    void RotateRight() 
    {
        CameraOrbit.transform.Rotate(new Vector3(0.0f, RotationSpeed * Time.deltaTime, 0.0f));
    }

    void RaycastFromMouse()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found! Make sure your camera has the 'MainCamera' tag.");
            return;
        }

        // Create a ray from the mouse cursor position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log("Hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Environment")) 
            {
                DestroyRock(hit.collider.gameObject);
            }

            if (hit.collider.gameObject.CompareTag("Win")) 
            {
                HitWinCrytal(hit.collider.gameObject);


            }

            
        }

        // Draw debug ray (visible in Scene View)
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
    }

    public void DestroyRock(GameObject RockObj) 
    {
        Destroy(RockObj);
    }

    public void HitWinCrytal(GameObject WinObject) 
    {
        Debug.Log("Win");
        microgameHandler.WinWhenTimeIsUp();

        Instantiate(Explosion, WinObject.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

        Destroy(WinObject);
    }
}

