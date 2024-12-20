using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MicrogameLoader : MonoBehaviour
{

    [Header("Use this script for microgame loading only")]
    public string ActiveSceneName;

    public static MicrogameLoader instance;

    public static event Action OnStartSceneChange;
    public static event Action OnOldSceneUnloaded;
    public static event Action OnNewSceneLoaded;
    public static event Action OnTimerStart;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Co_LoadMicrogame(sceneName));
    }

    public void UnloadCurrentMicrogame()
    {
        StartCoroutine(Co_UnloadCurrentMicrogame());
    }

    IEnumerator Co_UnloadCurrentMicrogame()
    {
        if (ActiveSceneName != "")
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(ActiveSceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            OnOldSceneUnloaded?.Invoke();
        }
    }

    IEnumerator Co_LoadMicrogame(string sceneName)
    {
        OnStartSceneChange?.Invoke();

        ActiveSceneName = sceneName;

        var sceneLoadOperation = SceneManager.LoadSceneAsync(ActiveSceneName, LoadSceneMode.Additive);

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(ActiveSceneName));

        OnNewSceneLoaded?.Invoke();

        OnTimerStart?.Invoke();
    }
}
