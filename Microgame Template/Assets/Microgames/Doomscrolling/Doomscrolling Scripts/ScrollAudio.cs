using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class ScrollAudio : MonoBehaviour
    {
        Scroll scroll;
        AudioSource audioS;
        public AudioSource handAudioS;
        public AnimationCurve pitchCurve;
        public Vector2 pitchRange;
        public Vector2 handPitchRange;
        public float volSpeedReq;
        public float maxVol;
        public AudioClip[] downUp;

        void Awake()
        {
            scroll = GetComponent<Scroll>();
            audioS = GetComponent<AudioSource>();
        }

        void Update()
        {
            float curveVal = pitchCurve.Evaluate(scroll.pos / scroll.bounds.y);
            audioS.pitch = Mathf.Lerp(pitchRange.x, pitchRange.y, curveVal);
            handAudioS.pitch = Mathf.Lerp(handPitchRange.x, handPitchRange.y, curveVal);
            audioS.volume = scroll.pos < scroll.bounds.y ? Mathf.Clamp01(scroll.scrollVel / volSpeedReq) * maxVol : 0;
        }

        public void MouseInteract(bool down)
        {
            handAudioS.PlayOneShot(down ? downUp[0] : downUp[1]);
        }
    }   
}
