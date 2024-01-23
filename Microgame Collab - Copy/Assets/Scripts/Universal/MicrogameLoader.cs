using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MicrogameLoader : MonoBehaviour
{

    [Header("Use this script for microgame loading only")]
    public string ActiveScene;

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
        if (ActiveScene != "")
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(ActiveScene);

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

        ActiveScene = sceneName;

        var sceneLoadOperation = SceneManager.LoadSceneAsync(ActiveScene, LoadSceneMode.Additive);

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }

        OnNewSceneLoaded?.Invoke();

        OnTimerStart?.Invoke();
    }
}
