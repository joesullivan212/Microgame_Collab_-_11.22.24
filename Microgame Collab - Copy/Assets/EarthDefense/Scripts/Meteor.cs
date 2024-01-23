using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Meteor : MonoBehaviour
{
    public float Speed = 2.0f;

    private Rigidbody2D rb;
    private Transform player;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        rb.velocity = ((player.position - gameObject.transform.position).normalized) * Speed;
    }

    public float FindAngleFunc(Vector2 vector)
    {
        float Angle = (Mathf.Atan2(vector.y, vector.x)) * 180.0f / Mathf.PI;
        return Angle;
    }
}
