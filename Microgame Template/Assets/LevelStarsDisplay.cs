using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarsDisplay : MonoBehaviour
{
    public int LevelIndex;

    public GameObject[] Stars;

    public Sprite Unfilled;
    public Sprite Filled;

    private void Update()
    {
       int AmountOfStarsToFill = PersistantData.instance.AreaScores[LevelIndex];

        foreach(GameObject star in Stars)
        {
            star.GetComponent<Image>().sprite = Unfilled;
        }

        for (int i = 0; i < AmountOfStarsToFill; i++)
        {
            if (i < Stars.Length)
            {
                Stars[i].GetComponent<Image>().sprite = Filled;
            }
        }
    }
}
