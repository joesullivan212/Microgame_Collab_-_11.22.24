using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomemadePlanet : MonoBehaviour
{
    public Rigidbody[] rigidbodies;

    public AreaManager areaManager;

    public AnimationCurve curve;

    public float Intensity;

    private List<Vector3> startingPositions = new List<Vector3>();

    private void Awake()
    {
        foreach(Rigidbody rb in rigidbodies) 
        {
            startingPositions.Add(rb.gameObject.transform.position);
        }
    }

    private void OnEnable()
    {
        Debug.Log("Homemade Planets Enabled");

        int i = 0;

        foreach(Rigidbody rb in rigidbodies)
        {
            rb.gameObject.transform.position = startingPositions[i];

            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            
            Vector3 dir = new Vector3(
                Random.Range(-1f, 1f),
                0.0f,
                Random.Range(-1.0f, 1.0f)
            ).normalized;

          // Debug.Log("Current Wins = " + areaManager.currentWins);
          // Debug.Log("Amount of microgames = " + areaManager.AmountOfMicroGames);
          // Debug.Log("Point on curve = " + areaManager.currentWins / areaManager.AmountOfMicroGames);
          // Debug.Log("Homemade Planet Force = " + curve.Evaluate(areaManager.currentWins / areaManager.AmountOfMicroGames) * Intensity);

            rb.AddForce(dir * curve.Evaluate(areaManager.currentWins / areaManager.AmountOfMicroGames) * Intensity, ForceMode.Impulse);

            i++;
        }
    }
}
