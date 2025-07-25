using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager instance;

    public FeedbackSender feedbackSender;
    
    public int UserID;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UserID = Random.Range(1, 9999); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SubmitFeedback(FeebackData feedbackData)
    {
        if (feedbackSender != null)
        {
            feedbackSender.SubmitFeedback(feedbackData, UserID.ToString());
        }
        else
        {
            Debug.LogError("FeedbackSender is not assigned in FeedbackManager.");
        }
    }
}
