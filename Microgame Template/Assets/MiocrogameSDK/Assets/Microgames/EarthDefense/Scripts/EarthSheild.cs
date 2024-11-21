using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSheild : MonoBehaviour
{
    public Camera camera;

    public float offset = 90.0f;

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, FindAngle(camera.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position) - offset);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent<Meteor>(out Meteor meteor))
        {
            return;
        }

        Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Meteor>(out Meteor meteor))
        {
            return;
        }

        Destroy(collision.gameObject);
    }

    public static float FindAngle(Vector2 vector)
    {
        float Angle = (Mathf.Atan2(vector.y, vector.x)) * 180.0f / Mathf.PI;
        return Angle;
    }
}
