using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSelector : MonoBehaviour
{
    public string AmountSelected = "";

    public SelectableStar[] SelectableStars;

    public Sprite UselectedSprite;

    public void Reset()
    {
        AmountSelected = "";

        foreach (var star in SelectableStars)
        {
            star.starImage.sprite = UselectedSprite;
        }
    }
}
