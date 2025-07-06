using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHScreenShake : MonoBehaviour
{

    public Transform cameraTransform;

    private Vector3 originalPosition;
    private float shakeDuration = 0.1f;  
    private float shakeIntensity = 0.05f;
    private float shakeTimer;

    void Start()
    {
        originalPosition = cameraTransform.position;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cameraTransform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
        }
        else
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, originalPosition, 10f * Time.deltaTime);
        }
    }

    public void StartShake()
    {
        shakeTimer = shakeDuration;
    }
}
