using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Debug.Log("Unloading scene: " + ActiveSceneName);

            var asyncOperation = SceneManager.UnloadSceneAsync(ActiveSceneName);

            if(asyncOperation == null)
            {
                Debug.LogError("Async operation is null, scene might not be loaded.");
            }

            Debug.Log("Waiting for scene to unload: " + ActiveSceneName);

            Debug.Log("Async operation progress: " + asyncOperation.progress.ToString());

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

        Debug.Log("Loading scene: " + ActiveSceneName);

        var sceneLoadOperation = SceneManager.LoadSceneAsync(ActiveSceneName, LoadSceneMode.Additive);

        Debug.Log("Waiting for scene to load: " + ActiveSceneName);

        Debug.Log("Async loading operation progress: " + sceneLoadOperation.progress.ToString());

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(ActiveSceneName));

        OnNewSceneLoaded?.Invoke();

        OnTimerStart?.Invoke();
    }
}
