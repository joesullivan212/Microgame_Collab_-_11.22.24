using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float Lifetime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", Lifetime);
    }

    private void DestroySelf() 
    {
        Destroy(gameObject);
    }
}
