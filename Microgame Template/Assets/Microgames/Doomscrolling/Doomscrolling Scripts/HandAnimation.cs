using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Doomscrolling
{
    public class HandAnimation : MonoBehaviour
    {
        public Scroll scroll;
        Animator anim;
        bool swiped;
        float swipeTime;
        float animTime;

        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            animTime += Time.deltaTime;

            if (!scroll.holding)
            {
                anim.Play("Hand.HandIdle", 0, animTime * 3);
                swiped = false;
                swipeTime = 0;
            }
            else if (swipeTime >= 0.065f)
            {
                anim.Play("Hand.HandSwipe", 0, animTime * 3);
                swiped = true;
            }
            else if (scroll.holding && !swiped)
            {
                anim.Play("Hand.HandPress", 0, animTime * 3);
            }

            swipeTime += scroll.holding && scroll.input.MouseMovement.y > 0 ? Time.deltaTime : 0;
        }
    }
}
