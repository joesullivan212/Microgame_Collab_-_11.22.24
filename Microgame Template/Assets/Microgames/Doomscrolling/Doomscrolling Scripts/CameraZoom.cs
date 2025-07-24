using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class CameraZoom : MonoBehaviour
    {
        public Scroll scroll;
        public Vector2 zoomBounds;

        void Update()
        {
            Camera.main.orthographicSize = Mathf.Lerp(zoomBounds.x, zoomBounds.y, scroll.pos / scroll.bounds.y);
        }
    }
}
