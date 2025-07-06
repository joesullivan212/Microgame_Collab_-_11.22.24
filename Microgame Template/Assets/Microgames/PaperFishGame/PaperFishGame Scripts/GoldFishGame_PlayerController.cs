using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGame_PlayerController : MonoBehaviour
{
    [SerializeField] MicrogameInputManager microgameIinputManager;

    public float ForceIntensity;
    public float OutOfWaterForceMultiplier;

    public float inputOffset;
    public float RotationSpeed;
    public Rigidbody rb;
    public GameObject CameraObj;
    public float DeclerationRate = 0.2f;

    [Header("VFX")]
    public GameObject Splash;
    public bool WasInAirLastFrame;
    public ParticleSystem WaterDroplets;
    public float WaterDropletsStrength = 1.0f;

    [Header("Camera")]
    public float CameraDepthMin;
    public float CameraDepthMax;
    public float SpeedNeededForMaxZoom;
    public float InAirCameraDepth;
    public float CameraLerpSpeed;


    [Header("Debug")]
    [SerializeField] float TargetCameraDepth;
    [SerializeField] bool UsingForce = false;
    [SerializeField] bool InAir = false;

    private Vector3 CameraOffset;
    
    private void Start()
    {
        CameraOffset = gameObject.transform.position - CameraObj.transform.position;

    }

    public void Update()
    {
        if (microgameIinputManager.MouseBeingHeld) 
        {
            UsingForce = true;  
        }
        else
        {
            UsingForce = false;
        }

        if(gameObject.transform.position.y > 0.0f) 
        {
            InAir = true;
        }
        else 
        {
            InAir = false;
        }

        //Camera
        CameraObj.transform.position = new Vector3(gameObject.transform.position.x - CameraOffset.x,
                                                    gameObject.transform.position.y - CameraOffset.y,
                                                    CameraObj.transform.position.z);
        //Gravity
        if(InAir == true) 
        {
            rb.useGravity = true;
        }
        else 
        {
            rb.useGravity = false;
        }

        //Camera
        if (InAir == false)
        {
            TargetCameraDepth = Mathf.Lerp(CameraDepthMin, CameraDepthMax, rb.velocity.magnitude / SpeedNeededForMaxZoom);
        }
        else 
        {
            TargetCameraDepth = InAirCameraDepth;
        }

        CameraObj.transform.position = new Vector3(CameraObj.transform.position.x,
                                                   CameraObj.transform.position.y,
                                                   Mathf.Lerp(CameraObj.transform.position.z, TargetCameraDepth, (CameraLerpSpeed * Time.deltaTime) ));
        



        //Splash
        if(WasInAirLastFrame != InAir) 
        { 
            Instantiate(Splash, gameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }

        WasInAirLastFrame = InAir;

        //Water droplets
        var emission = WaterDroplets.emission;
        emission.rateOverTime = rb.velocity.magnitude * WaterDropletsStrength;
    }

    void FixedUpdate()
     {
        if (UsingForce) 
        {
            //ROTATION
            Vector2 ScreenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

            Vector2 MousePosition = Input.mousePosition;

            Vector2 direction =  ScreenCenter - MousePosition;

            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            targetRotation += inputOffset;

            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.LerpAngle(gameObject.transform.eulerAngles.z, targetRotation, RotationSpeed * Time.deltaTime));

            if (InAir == false)
            {
                //rb.AddForce(gameObject.transform.right * (ForceIntensity * Time.deltaTime));
                rb.AddForce(direction.normalized * (-ForceIntensity * Time.deltaTime));
            }
            else 
            {
                rb.AddForce(direction.normalized * ((-ForceIntensity * OutOfWaterForceMultiplier) * Time.deltaTime));
            }
        }
        
        rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0.0f, 0.0f, 0.0f), DeclerationRate);
        

        
    }
}
