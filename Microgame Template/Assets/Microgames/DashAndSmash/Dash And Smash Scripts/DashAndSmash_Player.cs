using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Player : MonoBehaviour
{
    [SerializeField] private MicrogameHandler microgameHandler;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            microgameHandler.Lose();
        }
    }
}