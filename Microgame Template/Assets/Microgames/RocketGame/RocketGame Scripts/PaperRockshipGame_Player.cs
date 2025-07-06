using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PaperRockshipGame_Player : MonoBehaviour
{
    public MicrogameInputManager inputManager;

    public MicrogameHandler microgameHandler;

    public float RotationSpeed = 5.0f;

    public Vector2 ScreenCenter;

    public float Offset;

    public GameObject CameraObj;
    public Vector3 CameraOffset;

    [Header("Force")]
    public float Gravity = -9.8f;
    public float ForceIntensity;
    public Rigidbody rb;

    public bool UsingForce = false;

    [Header("Visuals")]
    public GameObject RocketsOn;
    public GameObject RocketsOff;

    [Header("Shoot")]
    public GameObject Bullet;
    public GameObject FiringLocation;
    public float FireRate;
    public float BulletSpeed;

    [Header("Death")]
    public bool Dead = false;
    public GameObject DeathFeedbackObj;
    public Rigidbody[] ScrapRigidbodies;

    [Header("Win")]
    public GameObject WinEffects;
    public bool Win = false;

    private void Start()
    {
        CameraOffset = gameObject.transform.position - CameraObj.transform.position;
        StartCoroutine("Shoot");
    }

    private void Update()
    {
        if (Dead == false)
        {
            //Camera
            CameraObj.transform.position = gameObject.transform.position - CameraOffset;

            //ROTATION
            ScreenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

            Vector2 MousePosition = Input.mousePosition;

            Vector2 direction = ScreenCenter - MousePosition;

            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            targetRotation += Offset;

            gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.LerpAngle(gameObject.transform.eulerAngles.z, targetRotation, RotationSpeed * Time.deltaTime));

            //FORCE
            if (inputManager.MouseBeingHeld)
            {
                UsingForce = true;
                rb.AddForce(gameObject.transform.up * (ForceIntensity * Time.deltaTime));
            }
            else
            {
                UsingForce = false;
            }



            //VISUALS
            if (UsingForce)
            {
                RocketsOn.SetActive(true);
                RocketsOff.SetActive(false);
            }
            else
            {
                RocketsOn.SetActive(false);
                RocketsOff.SetActive(true);
            }
        }

        
    }

    private void FixedUpdate()
    {
        rb.AddForce(-Vector3.up * Gravity);
    }

    IEnumerator Shoot() 
    {
        while (Dead == false && Win == false) 
        {
            yield return new WaitForSeconds(FireRate);

            GameObject NewBullet = Instantiate(Bullet, FiringLocation.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            NewBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * BulletSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win")) 
        {
            Destroy(other.gameObject);
            Debug.Log("CrystalFound");

            WinEffects.SetActive(true);

            Win = true;

            microgameHandler.WinWhenTimeIsUp();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Crashed");
            StartCoroutine("Crashed");
        }
    }

    IEnumerator Crashed() 
    {
        Dead = true;


        DeathFeedbackObj.SetActive(true);

        foreach(Rigidbody rb in ScrapRigidbodies) 
        {
            rb.velocity = gameObject.GetComponent<Rigidbody>().velocity;
        }

        RocketsOff.SetActive(false);
        RocketsOn.SetActive(false);

        microgameHandler.CancelTimer();
        yield return new WaitForSeconds(3.0f);
        microgameHandler.Lose();
    }
}
