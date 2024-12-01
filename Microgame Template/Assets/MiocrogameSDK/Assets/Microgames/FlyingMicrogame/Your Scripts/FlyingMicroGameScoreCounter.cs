using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FlyingMicroGameScoreCounter : MonoBehaviour
{
    public int CheckpointsScored;

    public GameObject CheckpointFeedbackObject;
    public MMF_Player CheckpointFeedback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) 
        {
            CheckpointsScored++;

            Instantiate(CheckpointFeedbackObject, transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            CheckpointFeedback.PlayFeedbacks();

            Destroy(other.gameObject);
        }
    }
}
