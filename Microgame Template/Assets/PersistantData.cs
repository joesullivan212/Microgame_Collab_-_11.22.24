using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance;

    public int[] AreaScores = new int[100];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void SaveScores() 
    {
        //SAVING GOES HERE
        Debug.Log("Code Saving Here!");
    }
}
