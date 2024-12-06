using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonDeathHandler : MonoBehaviour
{
    public GameObject Player;
    public GameObject Ribbon;
    public GameObject FeedbackObject;
    public MicrogameHandler microgameHandler;

    public void Death() 
    {
        microgameHandler.CancelTimer();

        //Ribbon.transform.parent = null;
        Instantiate(FeedbackObject, Player.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        Destroy(Player);

        Invoke("LoseGame", 1.0f);
    }

    public void LoseGame() 
    {
        microgameHandler.Lose();
    }
}
