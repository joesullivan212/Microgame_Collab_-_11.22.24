using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotShip : MonoBehaviour
{
    public Flying flying;
    public Rigidbody rb;
    public Transform PlayerSphere;

    public Vector3 offset;

    private void Start()
    {
        offset = PlayerSphere.position - transform.position;
    }

    void FixedUpdate()
    {
        transform.LookAt(PlayerSphere);

        transform.position = PlayerSphere.transform.position - offset;
    }
}
