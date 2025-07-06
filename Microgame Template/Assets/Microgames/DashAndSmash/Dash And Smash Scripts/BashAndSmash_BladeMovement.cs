using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_BladeMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform pivotPoint; 
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

        Vector3 direction = mousePosition - player.position;
        direction.y = 0;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        pivotPoint.rotation = Quaternion.Euler(0, -angle, 0);
    }
}