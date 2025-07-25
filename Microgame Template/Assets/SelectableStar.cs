using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectableStar : MonoBehaviour
{
    public StarSelector StarSelector;
    public string StarAmount = "";
    public SelectableStar[] PreviousStars;
    public SelectableStar[] AllStars;

    public Sprite SelectedStarIcon;
    public Sprite UnselectedStarIcon;

    public Image starImage;

    public void SelectStar()
    {
        StarSelector.AmountSelected = StarAmount;

        foreach (var star in AllStars)
        {
            star.starImage.sprite = UnselectedStarIcon;
        }

        foreach (var star in PreviousStars)
        {
            star.starImage.sprite = SelectedStarIcon;
        }
        starImage.sprite = SelectedStarIcon;
    }
}
