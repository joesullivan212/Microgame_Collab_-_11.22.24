using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinsDisplay : MonoBehaviour
{
    [SerializeField] GameObject WinIcon;

    public float DelayBeforeInstantiating = 1.0f;

    public void AddWinIcon()
    {
        StartCoroutine(AddWinIconCo());
    }

    IEnumerator AddWinIconCo()
    {
        //PLAY ANIMATION
        yield return new WaitForSeconds(DelayBeforeInstantiating);
        Instantiate(WinIcon, transform);
    }
}
