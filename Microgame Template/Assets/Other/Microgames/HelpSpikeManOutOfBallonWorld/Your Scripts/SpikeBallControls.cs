using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallControls : MonoBehaviour
{

    public Rigidbody rb;

    public float Intensity;

    public bool CanMove = true;

    public MicrogameInputManager microgameInputManager;

    public MicrogameHandler microgameHandler;

    public int Points;

    private void Update()
    {
        if (microgameInputManager.Clicked && CanMove)
        {
            rb.AddForce(new Vector3(Intensity, 0.0f, 0.0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    Points++;
        //}

        if (other.CompareTag("Environment")) 
        {
            CanMove = false;
        }

        if (other.CompareTag("Win")) 
        {
            microgameHandler.WinWhenTimeIsUp();
        }
    }
}
