using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightshift
{
    public class StaticAlpha : MonoBehaviour
    {
        public float alpha;
        public float desiredAlpha;
        public float time;
        SpriteRenderer rend;

        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            rend.color = new Color(1, 1, 1, alpha);
            alpha = Mathf.Lerp(alpha, desiredAlpha, time * Time.deltaTime);
        }

        public void SetAlpha(float a)
        {
            alpha = a;
        }
    }
}
