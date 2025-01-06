using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTheFutureWinBox : MonoBehaviour
{
    public SeeTheFutureGameController controller;

    public bool HasWon = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Debug.Log("Win box hit");

            if (HasWon == false)
            {
                controller.WinFunc();
                HasWon = true;
            }
        }
    }
}
