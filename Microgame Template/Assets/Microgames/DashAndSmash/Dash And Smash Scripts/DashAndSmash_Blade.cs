using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Blade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<DashAndSmash_Enemy>().StartCoroutine("DestroyEnemy");
        }
    }
}