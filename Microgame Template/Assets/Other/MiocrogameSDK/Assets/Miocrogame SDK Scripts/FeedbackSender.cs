using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FeedbackSender : MonoBehaviour
{

    private string formUrl = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfJ2OtyxAvcjQDX4J30xcwufdqbCVkP8tOnr-YrJeDpL-6HxA/formResponse";

    public void SubmitFeedback(FeebackData feebackData, string UserID)
    {
        StartCoroutine(Post(feebackData, UserID));
    }

    private IEnumerator Post(FeebackData feebackData, string UserID)
    {
        WWWForm form = new WWWForm();

        //Microgame
        form.AddField("entry.1018093848", feebackData.MicrogameName);

        //User ID
        form.AddField("entry.242923883", UserID.ToString());

        //Overall
        form.AddField("entry.401216540", feebackData.Overall.ToString());

        //Fun
        form.AddField("entry.1179622209", feebackData.Fun.ToString());

        //Difficulty
        form.AddField("entry.74895648", feebackData.Difficulty.ToString());

        //Confusion
        form.AddField("entry.943851298", feebackData.Understandable.ToString());

        //Comments
        form.AddField("entry.1075352038", feebackData.Comments);


        using (UnityWebRequest www = UnityWebRequest.Post(formUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Feedback submitted successfully.");
            }
            else
            {
                Debug.LogError("Error in feedback submission: " + www.error);
            }
        }
    }
}
