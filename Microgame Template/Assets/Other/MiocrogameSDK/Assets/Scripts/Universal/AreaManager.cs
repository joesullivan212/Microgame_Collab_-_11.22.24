using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    public int Lives = 3;
    public int winsNeeded = 5;
    public int currentWins = 0;

    [Header("Variables")]
    public float DisplayResultsDuration = 3.0f;
    public float gameNameDisplayDuration = 1.0f;
    public float gameGoalDisplayDuration = 1.0f;
    public float gameControlsDisplayDuration = 1.0f;
    public float SecondsBetweenCameraRisingAndDisplayingNextGameInformation = 0.75f;
    [SerializeField]
    private int AmountOfMicrogamesInArea = 4;
    public Animator CameraAnimator;


    [Header("Scene Refrences")]
    [SerializeField]
    private GameObject AreaWinScreen;
    [SerializeField]
    private GameObject AreaLoseScreen;
    [SerializeField]
    private GameObject WinFeedbackObject;
    [SerializeField]
    private GameObject LoseFeedbackObject;
    [SerializeField]
    private LivesDisplay livesDisplay;
    [SerializeField]
    private WinsDisplay WinsDisplay;
    [SerializeField]
    private TextMeshProUGUI gameNameText;
    [SerializeField]
    private TextMeshProUGUI gameGoalText;
    [SerializeField]
    private TextMeshProUGUI gameControlsText;

    [SerializeField]
    private MicrogameLibrary microgameLibrary;

    [SerializeField]
    private GameObject AreaSceneObjs;

    [Header("Debug")]
    [SerializeField]
    private Queue<D_Microgame> microgameQueue = new();

 //  [SerializeField]
 //  private D_Microgame[] microgamesQueueMirror;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(this);
    }

 //   private void Update()
 //   {
 //       microgamesQueueMirror = microgameQueue.ToArray();
 //   }

    private void OnEnable()
    {
        MicrogameLoader.OnNewSceneLoaded += DeactivateAreaSceneObj;
        MicrogameHandler.OnWin += Win;
        MicrogameHandler.OnLose += Lose;
    }

    private void OnDisable()
    {
        MicrogameLoader.OnNewSceneLoaded -= DeactivateAreaSceneObj;
        MicrogameHandler.OnWin -= Win;
        MicrogameHandler.OnLose -= Lose;
    }

    private void DeactivateAreaSceneObj()
    {
        AreaSceneObjs.SetActive(false);
    }

    private void Start()
    {
        Debug.Log("Amount of available games : " + microgameLibrary.microgames.Length);

        GenerateQueue();

        StartCoroutine(instance.NextMicrogame(false, true));
    }

    private void GenerateQueue()
    {
        List<int> QueueIndexes = new();

        for (int i = 0; i < microgameLibrary.microgames.Length; i++)
        {
            QueueIndexes.Add(GetUniqueInt(microgameLibrary.microgames.Length, QueueIndexes));
        }

        foreach (int Index in QueueIndexes)
        {
            microgameQueue.Enqueue(microgameLibrary.microgames[Index]);
        }
    }

    public int GetUniqueInt(int IntMax, List<int> CurrentQueue)
    {
        int UniqueIndex = UnityEngine.Random.Range(0, IntMax);

        if (!CurrentQueue.Contains(UniqueIndex))
            return UniqueIndex;

        else
            return GetUniqueInt(IntMax, CurrentQueue);
    }

    IEnumerator NextMicrogame(bool DidPlayerWinGame, bool Initializing)
    {
        CameraAnimator.Play("Idle");

        MicrogameLoader.instance.UnloadCurrentMicrogame();

        var NextGame = microgameQueue.Dequeue();

        ClearTexts();

        yield return StartCoroutine(DisplayResults(DidPlayerWinGame, Initializing));

        if (DidPlayerWinOrLoseArea() == false)
        {
            yield return StartCoroutine(DisplayNextGame(NextGame));

            MicrogameLoader.instance.LoadScene(NextGame.GameSceneName);
        }
        else 
        {
            WinOrLoseArea();
        }
    }

    IEnumerator DisplayResults(bool DidWinLastGame, bool IsInitializing) 
    {

        if (IsInitializing == false)
        {
            if (DidWinLastGame == true)
            {
                WinFeedbackObject.SetActive(true);
                WinsDisplay.AddWinIcon();
                yield return new WaitForSeconds(DisplayResultsDuration);
                CameraAnimator.Play("Rise");
            }
            if (DidWinLastGame == false)
            {
                LoseFeedbackObject.SetActive(true);
                livesDisplay.SubtractLife();
                yield return new WaitForSeconds(DisplayResultsDuration);
                CameraAnimator.Play("Rise");
            }
        }
    }

    IEnumerator DisplayNextGame(D_Microgame NextGame) 
    {
        yield return new WaitForSeconds(SecondsBetweenCameraRisingAndDisplayingNextGameInformation);
        gameNameText.text = NextGame.GameName;
        yield return new WaitForSeconds(gameNameDisplayDuration);
        gameGoalText.text = NextGame.GameGoal;
        yield return new WaitForSeconds(gameGoalDisplayDuration);
        gameControlsText.text = NextGame.GameControls;
        yield return new WaitForSeconds(gameControlsDisplayDuration);
        WinFeedbackObject.SetActive(false);
        LoseFeedbackObject.SetActive(false);

    }

    public bool DidPlayerWinOrLoseArea() 
    {
        if (currentWins >= winsNeeded)
        {
            return true;
        }

        if(Lives <= 0) 
        {
            return true;
        }

        return false;
    }

    private void ClearTexts()
    {
        gameNameText.text = string.Empty;
        gameGoalText.text = string.Empty;
        gameControlsText.text = string.Empty;
    }


    private void Win()
    {
        currentWins++;

        AreaSceneObjs.SetActive(true);

        if (microgameQueue.Count <= 0)
            GenerateQueue();

        StartCoroutine(NextMicrogame(true, false));
    }

    private void Lose()
    {
        instance.Lives--;

        instance.AreaSceneObjs.SetActive(true);

        if (instance.microgameQueue.Count <= 0)
        {
            instance.GenerateQueue();
        }

        instance.StartCoroutine(instance.NextMicrogame(false, false));
    }

    private void WinOrLoseArea() 
    {
        if (currentWins >= winsNeeded)
        {
            AreaWin();
        }

        if (Lives <= 0)
        {
            AreaLose();
        }
    }

    private void AreaWin()
    {
        Debug.Log("You Won The Area!");
        AreaWinScreen.SetActive(true);
    }

    private void AreaLose() 
    {
        Debug.Log("You Lost The Area!");
        AreaLoseScreen.SetActive(true);
    }
}
