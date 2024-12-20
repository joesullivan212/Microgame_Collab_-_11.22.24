using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public float Momentum;

    public Camera Camera;
    public Vector3 ScreenCenter;
    public Rigidbody rb;
    public float LerpSpeed;

    [Header("Variables")]
    public float RatioOfTurnSpeedToMomentum;
    public float RationOfForwardSpeedToMomentum = 0.2f;
    public float TurnSpeed = 0.1f;
    public float MaxMagnitude = 0.2f;

    [Header("Debug")]
    public Vector2 RawInputDirection;
    public float RawMagnitude;
    public Vector2 InputDirection;

    private void Start()
    {
        ScreenCenter.x = Screen.width * 0.5f;
        ScreenCenter.y = Screen.height * 0.5f;
    }

    private void Update()
    {
       RawInputDirection = Input.mousePosition - ScreenCenter;

       RawMagnitude = Mathf.Abs(RawInputDirection.magnitude);
       
       RawMagnitude = RawMagnitude / Screen.width;
        
       InputDirection = RawInputDirection;
    }

    private void FixedUpdate()
    {
        Vector3 TargetVelocity = new Vector3(InputDirection.x  * TurnSpeed, InputDirection.y * TurnSpeed, Momentum);

        rb.velocity = Vector3.Lerp(rb.velocity, TargetVelocity, LerpSpeed);
    }
}
