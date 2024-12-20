using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionChanger : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 Pos1;
    public Vector3 Pos2;
    public Vector3 TargetPosition;
    public float MaxVelocity;
    public float Smoothness = 0.25f;

    private void Update()
    {
        float LerpScale = rb.velocity.magnitude / MaxVelocity;

        if(LerpScale > 1.0f) 
        {
            LerpScale = 1.0f;
        }

        TargetPosition = Vector3.Lerp(Pos1, Pos2, LerpScale);
        transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, Smoothness * Time.deltaTime);
    }
}
