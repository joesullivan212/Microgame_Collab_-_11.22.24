using System.Collections;
using System.Collections.Generic;
using UndertaleBattle;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public Sprite[] sprites;
    public Gradient gradient;
    SpriteRenderer rend;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        UpdateDisplay(GameObject.Find("Heart").GetComponent<HeartManager>().health);
    }

    public void UpdateDisplay(int hp)
    {
        rend.sprite = sprites[hp - 1];
        rend.color = gradient.Evaluate((float)hp / sprites.Length);
    }
}
