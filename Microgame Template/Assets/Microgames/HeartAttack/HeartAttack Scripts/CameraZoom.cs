using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndertaleBattle
{
    public class CameraZoom : MonoBehaviour
    {
        public Timer timer;
        public Vector2 startEndZoom;
        public AnimationCurve curve;

        void Update()
        {
            Camera.main.orthographicSize = Mathf.Lerp(startEndZoom.y, startEndZoom.x, curve.Evaluate(timer.remainingTime / timer.maxTime));
        }
    }
}
