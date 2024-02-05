using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PingFeedback : MonoBehaviour
{
    public TextMeshPro DistanceFromTargetText;
    public GameObject PingObject;
    public Gradient DistanceGradient;
    public float PingScaleMultiplier = 1.0f;
    public float ColorScaleMultiplier = 1.0f;

    public void PlayPingFeedback(float Distance)
    {
        DistanceFromTargetText.text = Distance.ToString("0.0");
        DistanceFromTargetText.color = DistanceGradient.Evaluate(Distance * ColorScaleMultiplier);
        PingObject.transform.localScale = new Vector3(Distance / PingScaleMultiplier, Distance / PingScaleMultiplier, 0.0f);
    }
}
