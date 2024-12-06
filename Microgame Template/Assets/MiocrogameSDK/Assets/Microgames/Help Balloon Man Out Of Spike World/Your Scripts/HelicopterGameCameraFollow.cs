using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGameCameraFollow : MonoBehaviour
{
    public Transform followTransform;

    public Vector3 offset;

    private void Start()
    {
        offset = gameObject.transform.position - followTransform.transform.position;
    }
    public void Update()
    {
        if (followTransform != null)
        {
            transform.position = new Vector3(followTransform.position.x + offset.x, transform.position.y, followTransform.position.z + offset.z);
        }
    }
}
