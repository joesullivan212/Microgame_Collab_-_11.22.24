using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public Light light;

   // public float StartingIntensity;
   // public float StartingRange;

    public float DecreaseLerp;

    private void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, 0, DecreaseLerp * Time.deltaTime);
        light.range = Mathf.Lerp(light.range, 0, DecreaseLerp * Time.deltaTime);
    }
}
