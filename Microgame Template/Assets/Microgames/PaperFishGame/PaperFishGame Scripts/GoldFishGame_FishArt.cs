using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGame_FishArt : MonoBehaviour
{
    public Transform ParentObject;
    public Animator anim;

    public bool GoingRight = true;

    [Header("Debug")]
    public float ZRot;
    
    private void Update()
    {
        ZRot = ParentObject.localEulerAngles.z;
        if((ZRot > 0.0f && ZRot < 90.0f) || (ZRot > 270.0f)) 
        {
            GoingRight = true;
        }
        else 
        {
            GoingRight = false;
        }

        anim.SetBool("GoingRight", GoingRight);
    }
}
