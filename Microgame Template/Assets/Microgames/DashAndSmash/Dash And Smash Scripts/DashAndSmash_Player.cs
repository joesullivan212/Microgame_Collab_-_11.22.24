using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Player : MonoBehaviour
{
    [SerializeField] private MicrogameHandler microgameHandler;

    private bool GameRunning = true;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameRunning == true)
            {
                microgameHandler.Lose();
                GameRunning = false;
            }
        }
    }
}