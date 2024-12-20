using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautDanceController : MonoBehaviour
{
    public AnimationClip[] animations;
    public Animator animator;

    public void Dance() 
    {
        int RNG = Random.Range(0, animations.Length);

        AnimationClip animationClip = animations[RNG];

        float RandomTime = Random.Range(0.0f, animationClip.length);

        animator.Play(animationClip.name, 0, RandomTime);
    
    }
}
