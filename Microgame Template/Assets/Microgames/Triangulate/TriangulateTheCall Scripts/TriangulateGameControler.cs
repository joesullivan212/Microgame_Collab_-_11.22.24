using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PotentialTargetLocation
{
    public string LocationName;
    public Vector3 LocationInGame;
}

public class TriangulateGameControler : MonoBehaviour
{
    [SerializeField]
    public PotentialTargetLocation[] potentialTargetLocations;

    [Header("Variables")]
    public float ZPosition;
    public float TargetDistanceAccepted = 2.0f;

    [Header("Plug Comps")]
    public Camera camera;
    public GameObject PingFeedbackObj;
    public MicrogameHandler microgameHandler;
    public Timer Timer;
    public GameObject WinObject;
    public GameObject TargetFoundFeedback;
    public Transform PingHolder;
    public MicrogameInputManager microgameInputManager;

    [Header("Debug")]
    [SerializeField]
    private PotentialTargetLocation SelectedTargetLocation;

    private void Start()
    {
        SelectedTargetLocation = potentialTargetLocations[Random.Range(0, potentialTargetLocations.Length)];
    }

    void Update()
    {
        //Spawn Feedback Objs
        if (microgameInputManager.Clicked)
        {
            Vector3 Pos = camera.ScreenToWorldPoint(microgameInputManager.MouseScreenPosition);

            Vector3 EditedPosition = new Vector3(Pos.x, Pos.y, ZPosition);

            GameObject NewPingFeedback = Instantiate(PingFeedbackObj, EditedPosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));

            //NewPingFeedback.transform.parent = PingHolder;

            float DistanceToTarget = Vector2.Distance(NewPingFeedback.transform.position, SelectedTargetLocation.LocationInGame);

            if(DistanceToTarget < TargetDistanceAccepted)
            {
                Timer.CancelTimer();

                Invoke("WinAfterDelay", 1.0f);

                WinObject.SetActive(true);

                GameObject ThisTargetFoundFeedback =  Instantiate(TargetFoundFeedback, SelectedTargetLocation.LocationInGame, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                ThisTargetFoundFeedback.transform.parent = PingHolder;

                WinObject.GetComponent<TextMeshProUGUI>().text = "TARGET LOCATED" + "\n" + SelectedTargetLocation.LocationName;
            }

            NewPingFeedback.GetComponent<PingFeedback>().PlayPingFeedback(DistanceToTarget);
        }
    }

    public void WinAfterDelay()
    {
        Debug.Log("Win");
        microgameHandler.Win();

    }
}
