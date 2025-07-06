using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGame_Hoop : MonoBehaviour
{
    public GoldFishGame_GameController goldFishGame_GameController;

    public bool HoopComplete = false;

    public GoldFishGame_HoopTrigger[] hoopTriggers;

    [Header("Vari")]
    public float ResetFromDistanceThreshold = 14.0f;
    public float TimeBetweenHoopCompleteGameObjectSpawns;

    [Header("Refs")]
    public Transform player;
    public GameObject HoopCompleteFeedback;
    public GameObject[] HoopCompleteGameObjectToActivate;

    [Header("Debug")]
    public float DistanceFromPlayer;



    private void Update()
    {
        

        if (HoopComplete == false) 
        { 


            bool allTriggersComplete = true;
           
            foreach (GoldFishGame_HoopTrigger hoopTrigger in hoopTriggers)
            {
                if (hoopTrigger.HasTriggered == false)
                {
                    allTriggersComplete = false;
                }
            }
           
            if (allTriggersComplete)
            {
                HoopComplete = true;
                FinishHoop();
            }

            //Check if it needs to reset
            DistanceFromPlayer = Vector3.Distance(transform.position, player.position);
            if (DistanceFromPlayer > ResetFromDistanceThreshold) 
            {
                ResetHoop();
            }
        }
    }

    void FinishHoop() 
    {
        goldFishGame_GameController.HoopsScored++;

        Instantiate(HoopCompleteFeedback, gameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

        StartCoroutine("ActivateFeedbackObjs");
    }

    void ResetHoop() 
    { 
       foreach(GoldFishGame_HoopTrigger hooptrigger in hoopTriggers) 
       {
            hooptrigger.HasTriggered = false; 
       }
    }

    IEnumerator ActivateFeedbackObjs ()
    {
        foreach (GameObject HoopCompleteGameObject in HoopCompleteGameObjectToActivate)
        {
            yield return new WaitForSeconds(TimeBetweenHoopCompleteGameObjectSpawns);

            HoopCompleteGameObject.SetActive(true);
        }
    }
}
