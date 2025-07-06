using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaInfo 
{
    public string AreaName;
    public MicrogameLibrary microgameLibrary;
}
public class NightShift_Data : MonoBehaviour
{
    public static NightShift_Data instance;

    [SerializeField]
    public List<AreaInfo> Areas = new List<AreaInfo>();

    public void Awake()
    {
        if(instance == null) 
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
