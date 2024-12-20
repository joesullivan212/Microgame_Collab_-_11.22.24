using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using TMPro;

public class FlyingMicroGameScoreCounter : MonoBehaviour
{
    public int CheckpointsScored;

    public GameObject CheckpointFeedbackObject;
    public MMF_Player CheckpointFeedback;

    public GameObject CheckPointText;
    public Transform CheckpointTextLocation;

    public WinCondition winCondition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) 
        {
            CheckpointsScored++;

            Instantiate(CheckpointFeedbackObject, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            GameObject checkpointText = Instantiate(CheckPointText, CheckpointTextLocation);
            checkpointText.GetComponent<TextMeshPro>().text = CheckpointsScored.ToString() + "/" + winCondition.CheckpointsNeeded.ToString();

            CheckpointFeedback.PlayFeedbacks();

            Destroy(other.gameObject);
        }
    }
}
