using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGameCameraFollow : MonoBehaviour
{
    public Transform followTransform;

    public Vector3 offset;

    public bool XFollow = true;
    public bool YFollow = false;

    private void Start()
    {
        offset = gameObject.transform.position - followTransform.transform.position;
    }

    public void Update()
    {
        if (followTransform != null)
        {
            if (XFollow)
            {
                transform.position = new Vector3(followTransform.position.x + offset.x, transform.position.y, followTransform.position.z + offset.z);
            }
            else if (YFollow) 
            {
                transform.position = new Vector3(transform.position.x, followTransform.position.y + offset.y, followTransform.position.z + offset.z);
            }
        }
    }
}
