using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Microgame", menuName = "Scriptable Objects / Microgame")]
public class D_Microgame : ScriptableObject
{

    [Header("Splash Screen Data")]
    [SerializeField]
    private string gameName;

    [SerializeField, Multiline]
    private string gameGoal;

    [SerializeField, Multiline]
    private string gameControls;

    [Header("Microgame Data")]
    [SerializeField, Tooltip("Must match name of the Microgame scene")]
    private string gameSceneName;
    [SerializeField]
    private float timerDuration = 8.0f;
    [SerializeField, Tooltip("Defines whether or not the player wins or loses when time runs out")]
    Outcome outcomeOnTimeUp;

    [Header("Tags")]
    [SerializeField]
    private bool requiresSound;
    [SerializeField]
    private bool requiresColor;


    //getters

    public string GameName => gameName;
    public string GameGoal => gameGoal;
    public string GameControls => gameControls;
    public float TimerDuration => timerDuration;
    public Outcome OutcomeOnTimeUp => outcomeOnTimeUp;

    public string GameSceneName => gameSceneName;
    public bool RequiresSound => requiresSound;
    public bool RequiresColor => requiresColor;

}

public enum Outcome
{
    Win, Lose
}
