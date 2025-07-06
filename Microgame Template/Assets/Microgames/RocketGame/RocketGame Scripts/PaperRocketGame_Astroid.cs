using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperRocketGame_Astroid : MonoBehaviour
{
    public int StartingHealth;
    public int Health = 8;

    public MMF_Player hurtFeedback;

    public Transform HealthBar;

    public GameObject SpawnOnDeathObj;

    [Header("Debug")]
    public float HealthScale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) 
        {
            Destroy(other.gameObject);
            Hit();
        }
    }

    void Hit() 
    {
        hurtFeedback.PlayFeedbacks();

        Health--;
    }

    private void Update()
    {
        HealthScale = (float)Health / (float)StartingHealth;
        HealthBar.localScale = new Vector3 (HealthBar.localScale.x,
                                            HealthScale,
                                            HealthBar.localScale.z);

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death() 
    {
        Instantiate(SpawnOnDeathObj, gameObject.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        Destroy(gameObject);
    }
}
