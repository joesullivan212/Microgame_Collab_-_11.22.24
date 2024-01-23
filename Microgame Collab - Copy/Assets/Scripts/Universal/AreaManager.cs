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
    public float gameNameDisplayDuration = 1.0f;
    public float gameGoalDisplayDuration = 1.0f;
    public float gameControlsDisplayDuration = 1.0f;
    [SerializeField]
    private int AmountOfMicrogamesInArea = 4;


    [Header("Scene Refrences")]
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

    [SerializeField]
    private D_Microgame[] microgamesQueueMirror;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(this);
    }

    private void Update()
    {
        microgamesQueueMirror = microgameQueue.ToArray();
    }

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

        StartCoroutine(instance.LoadMicrogame());
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

    IEnumerator LoadMicrogame()
    {
        MicrogameLoader.instance.UnloadCurrentMicrogame();

        var NextGame = microgameQueue.Dequeue();

        ClearTexts();
        gameNameText.text = NextGame.GameName;
        yield return new WaitForSeconds(gameNameDisplayDuration);
        gameGoalText.text = NextGame.GameGoal;
        yield return new WaitForSeconds(gameGoalDisplayDuration);
        gameControlsText.text = NextGame.GameControls;
        yield return new WaitForSeconds(gameControlsDisplayDuration);

        MicrogameLoader.instance.LoadScene(NextGame.GameSceneName);
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

        if (currentWins >= winsNeeded)
        {
            AreaWin();
            MicrogameLoader.instance.UnloadCurrentMicrogame();
            AreaSceneObjs.SetActive(true);
            return;
        }

        AreaSceneObjs.SetActive(true);

        if (microgameQueue.Count <= 0)
            GenerateQueue();

        StartCoroutine(LoadMicrogame());
    }

    private void Lose()
    {
        instance.Lives--;

        if (instance.Lives <= 0)
        {
            Debug.Log("LOSE, You are garbage trash from a toilet");
            MicrogameLoader.instance.UnloadCurrentMicrogame();
            instance.AreaSceneObjs.SetActive(true);
            return;
        }

        instance.AreaSceneObjs.SetActive(true);


        if (instance.microgameQueue.Count <= 0)
        {
            instance.GenerateQueue();
        }

        instance.StartCoroutine(instance.LoadMicrogame());
    }

    private void AreaWin()
    {
        Debug.Log("You Won The Area!");
    }
}
