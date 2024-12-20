using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Microgame Library", menuName = "Scriptable Objects / Microgame Library")]
public class MicrogameLibrary : ScriptableObject
{

    public D_Microgame[] microgames;

    public D_Microgame GetRandom()
    {
        return microgames[Random.Range(0, microgames.Length)];
    }
}
