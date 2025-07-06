using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public FlyingMicroGameScoreCounter FlyingMicroGameScoreCounter;
    public TextMeshProUGUI ScoreText;
    public WinCondition winCondition;

    public void Update()
    {
        ScoreText.text = FlyingMicroGameScoreCounter.CheckpointsScored.ToString() + " / " + winCondition.CheckpointsNeeded;
    }
}
