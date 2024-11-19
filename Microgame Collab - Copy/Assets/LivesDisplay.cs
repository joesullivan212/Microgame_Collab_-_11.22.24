using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] GameObject HeartIcon;
    [SerializeField] AreaManager areaManager;

    public List<GameObject> HeartIcons = new List<GameObject>();

    [SerializeField] float AnimationLength = 1.0f;

    public void Start()
    {
        for(int i = 0; i < areaManager.Lives; i++) 
        {
            GameObject NewHeartIcon = Instantiate(HeartIcon, transform);
            HeartIcons.Add(NewHeartIcon);
        }
    }
    public void SubtractLife() 
    {
        StartCoroutine(SubtractLifeDisplay());
    }

    IEnumerator SubtractLifeDisplay() 
    {
        //PLAY ANIMATION
        yield return new WaitForSeconds(AnimationLength);
        HeartIcons[HeartIcons.Count - 1].SetActive(false);
        HeartIcons.RemoveAt(HeartIcons.Count - 1);
    }
}
