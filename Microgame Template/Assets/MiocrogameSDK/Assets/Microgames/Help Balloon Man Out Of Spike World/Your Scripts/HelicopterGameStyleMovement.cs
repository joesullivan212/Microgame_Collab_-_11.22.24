using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGameStyleMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float Force;

    public float ForwardVelocity;

    public MicrogameInputManager microgameInputManager;

    public BalloonDeathHandler balloonDeathHandler;


    private void FixedUpdate()
    {
        if (microgameInputManager.MouseBeingHeld) 
        {
            rb.AddForce(0.0f, Force, 0.0f);
        }

        rb.velocity = new Vector3(ForwardVelocity, rb.velocity.y, rb.velocity.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            balloonDeathHandler.Death();
        }
    }
}
