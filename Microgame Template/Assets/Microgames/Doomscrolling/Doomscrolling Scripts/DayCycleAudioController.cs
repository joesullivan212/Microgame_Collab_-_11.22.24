using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class DayCycleAudioController : MonoBehaviour
    {
        public Scroll scroll;
        public AnimationCurve volCurve;
        AudioSource audioS;

        void Awake()
        {
            audioS = GetComponent<AudioSource>();
        }

        void Update()
        {
            audioS.volume = volCurve.Evaluate(scroll.pos / scroll.bounds.y);
        }
    }
}
