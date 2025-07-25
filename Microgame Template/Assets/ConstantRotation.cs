using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 rotation;

    private void Start()
    {
        rotation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
    }

    private void FixedUpdate()
    {
        transform.Rotate(rotation * speed, Space.World);
    }
}
