using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightShift_LevelDisplay : MonoBehaviour
{
    [Header("Texture Refrences")]
    public Sprite EmptyStar;
    public Sprite FullStar;

    [Header("Component Refrences")]
    public Image[] StarImageComponents;

    public void DisplayLevel(int levelIndex) 
    { 
        int AmountOfStarsUnlocked = PersistantData.instance.AreaScores[levelIndex];

        Debug.Log("Amount of stars unlocked for this level is " + AmountOfStarsUnlocked.ToString());

        EmptyAllStars();

        FillStars(AmountOfStarsUnlocked);
    }

    public void EmptyAllStars() 
    {
        foreach (Image starIamge in StarImageComponents)
        {
            starIamge.sprite = EmptyStar;
        }
    }

    public void FillStars(int AmountOfStars) 
    {
      for(int i = 0; i < AmountOfStars; i++) 
      {
            StarImageComponents[i].sprite = FullStar;  
      }
    }
}
