using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGame_HoopTrigger : MonoBehaviour
{
    public bool HasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HasTriggered = true;
        }
    }
}
