using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public FlyingMicroGameScoreCounter FlyingMicroGameScoreCounter;
    public MicrogameHandler microgameHandler;

    public bool GameHasBeenWon = false;

    public int CheckpointsNeeded = 4;

    private void Update()
    {
        if(GameHasBeenWon == false) 
        { 
           if(FlyingMicroGameScoreCounter.CheckpointsScored >= CheckpointsNeeded) 
           {
                microgameHandler.WinWhenTimeIsUp();
                GameHasBeenWon = true;
           }
        }
    }
}
