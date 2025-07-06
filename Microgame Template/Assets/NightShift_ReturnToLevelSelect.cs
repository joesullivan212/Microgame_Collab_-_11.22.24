using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightShift_ReturnToLevelSelect : MonoBehaviour
{
    public int SceneIndex = 0;

    public void ReturnFunc() 
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
