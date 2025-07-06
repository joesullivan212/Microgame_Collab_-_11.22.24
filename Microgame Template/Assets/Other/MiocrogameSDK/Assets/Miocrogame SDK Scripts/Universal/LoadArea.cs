using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    public string AreaName;

    public void LoadAreaFunc()
    {
        SceneManager.LoadScene(AreaName);
    }
}
