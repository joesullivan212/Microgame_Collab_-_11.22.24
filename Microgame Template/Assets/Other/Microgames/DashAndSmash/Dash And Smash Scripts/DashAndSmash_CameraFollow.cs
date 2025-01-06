using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    private float fixedYPosition = 8.75f;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x, fixedYPosition, player.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}