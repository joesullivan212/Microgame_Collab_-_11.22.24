using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTheFutureRisingSmoke : MonoBehaviour
{
    public Renderer renderer;
    public float Speed;

    void Update()
    {
        renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x, renderer.material.mainTextureOffset.y + (Time.deltaTime * Speed));
    }
}
