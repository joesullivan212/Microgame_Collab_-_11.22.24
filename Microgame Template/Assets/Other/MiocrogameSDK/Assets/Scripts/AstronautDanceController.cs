using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautDanceController : MonoBehaviour
{
    public AnimationClip[] animations;
    public Animator animator;
    public int Index;

    public bool PlayRandom;

    public void Dance() 
    {
        if (PlayRandom)
        {
            Index = Random.Range(0, animations.Length);
        }
        
        AnimationClip animationClip = animations[Index % animations.Length];

        float RandomTime = Random.Range(0.0f, animationClip.length);

        animator.Play(animationClip.name, 0, RandomTime);

        Index++;
    }
}
