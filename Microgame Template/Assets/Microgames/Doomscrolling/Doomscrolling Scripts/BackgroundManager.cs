using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class BackgroundManager : MonoBehaviour
    {
        public Scroll scroll;
        public Gradient colGradient;
        public SpriteRenderer background;

        void Update()
        {
            Color col = colGradient.Evaluate(scroll.pos / scroll.bounds.y);
            Camera.main.backgroundColor = col;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = col;
            background.color = new Color(col.r, col.g, col.b, background.color.a);
        }
    }
}
