using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class SpriteMaskCopy : MonoBehaviour
    {
        SpriteMask mask;
        SpriteRenderer rend;

        void Awake()
        {
            mask = GetComponent<SpriteMask>();
            rend = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            mask.sprite = rend.sprite;
        }
    }
}
