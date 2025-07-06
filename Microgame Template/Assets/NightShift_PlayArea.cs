using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightShift_PlayArea : MonoBehaviour
{
    public int PlayAreaSceneIndex = 1;

    public NightShift_LevelSelector levelSelector;

    public void PlayAreaFunc() 
    {
        GameManager.instance.ActiveArea = levelSelector.AreaSelectedIndex;

        SceneManager.LoadScene(PlayAreaSceneIndex);
    }
}
