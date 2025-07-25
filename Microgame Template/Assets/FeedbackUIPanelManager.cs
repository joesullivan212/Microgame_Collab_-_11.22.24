using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUIPanelManager : MonoBehaviour
{
    public MicrogameLoader microgameLoader;
    public TextMeshProUGUI microgameNameText;
    public StarSelector OverallStars;
    public StarSelector FunStars;
    public StarSelector DifficultyStars;
    public StarSelector UnderstandableStars;
    public TextMeshProUGUI commentsInput;


    private void OnEnable()
    {
        ResetFeedback();

        microgameNameText.text = microgameLoader.ActiveSceneName;
    }

    public void SubmitFeedback() 
    { 
        FeebackData feedbackData = new FeebackData();

        feedbackData.MicrogameName = microgameLoader.ActiveSceneName;

        feedbackData.Overall = OverallStars.AmountSelected;
        feedbackData.Fun = FunStars.AmountSelected;
        feedbackData.Difficulty = DifficultyStars.AmountSelected;
        feedbackData.Understandable = UnderstandableStars.AmountSelected;

        feedbackData.Comments = commentsInput.text;

        FeedbackManager.instance.SubmitFeedback(feedbackData);
    }

    public void ResetFeedback()
    {
        OverallStars.Reset();
        FunStars.Reset();
        DifficultyStars.Reset();
        UnderstandableStars.Reset();
        commentsInput.text = "";
    }
}
