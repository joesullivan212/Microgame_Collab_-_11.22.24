using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashing : MonoBehaviour
{
    public ParticleSystem watersplashParticleSytem;
    public GameObject watersplashGameObject;
    public float waterLevel;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Touching Water");
            watersplashParticleSytem.Play();
            watersplashGameObject.transform.position = new Vector3(watersplashGameObject.transform.position.x,
                                                                   waterLevel,
                                                                   watersplashGameObject.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            watersplashParticleSytem.Stop();
        }
    }
}
