using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtleCameraFollow : MonoBehaviour
{
    public float followStrength = 0.1f; 
    public float maxOffset = 0.5f; 
    public float smoothSpeed = 5f; 

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public bool keepStill;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    void Update()
    {
        if (!keepStill)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            Vector3 offset = (mousePos - screenCenter) * followStrength;
            offset = new Vector3(
                Mathf.Clamp(offset.x, -maxOffset, maxOffset),
                Mathf.Clamp(offset.y, -maxOffset, maxOffset),
                0f);

            targetPosition = initialPosition + offset;


            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
        
    }
}
