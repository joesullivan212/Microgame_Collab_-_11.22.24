using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class Scroll : MonoBehaviour
    {
        public MicrogameInputManager input;
        public Vector2 bounds;
        public float offset;
        [Header("Scroll Speed Settings")]
        public float scrollSpeed;
        public float scrollDecay;
        [Header("Win Sequence")]
        public MicrogameHandler handler;
        public GameObject confetti;
        public float winWaitTime;
        bool won;
        [Header("Output")]
        public bool holding;
        public float pos;
        public float scrollVel;


        void OnMouseDown()
        {
            holding = true;
            GetComponent<ScrollAudio>().MouseInteract(true);
        }

        void OnMouseUp()
        {
            holding = false;
            GetComponent<ScrollAudio>().MouseInteract(false);
        }

        void Update()
        {
            if (!won)
            {
                if (holding) scrollVel = Mathf.Clamp(scrollVel + input.MouseMovement.y * scrollSpeed * Time.deltaTime, 0, Mathf.Infinity);
                pos = Mathf.Clamp(pos + scrollVel * Time.deltaTime, bounds.x, bounds.y);
                transform.GetChild(0).transform.position = new Vector3(0, pos + offset, 0);
                scrollVel = pos == bounds.x ? scrollVel = 0 : Mathf.Lerp(scrollVel, 0, scrollDecay * Time.deltaTime);
                if (pos == bounds.y) StartCoroutine(Win());
            }
        }

        IEnumerator Win()
        {
            won = true;
            handler.CancelTimer();
            Instantiate(confetti, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(winWaitTime);
            handler.Win();
        }
    }
}
