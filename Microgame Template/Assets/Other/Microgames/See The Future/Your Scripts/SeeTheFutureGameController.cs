using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTheFutureGameController : MonoBehaviour
{
    public MicrogameInputManager MicrogameInputManager;
    public MicrogameHandler MicrogameHandler;

    public SeperateCells seperateCells;
    public GameObject futureGameObject;

    public float InitRotationRange = 80.0f;

    public float ClampedRangePositive = 85.0f;
    public float ClampedRangeNegative = -275.0f;

    public float Sensitivity;

    public bool Clamp;

    public float WinDistance;

    public bool HasWon = false;

    private void Start()
    {
        seperateCells.SeperateCellsFunc();

        float XRNG = Random.Range(0, InitRotationRange);
        float YRNG = Random.Range(0, InitRotationRange);
        futureGameObject.transform.localEulerAngles = new Vector3(XRNG, 0.0f, YRNG);
        
        //cancel y
        futureGameObject.transform.rotation = Quaternion.Euler(futureGameObject.transform.localEulerAngles.x, 0.0f, futureGameObject.transform.localEulerAngles.z);
    }

    private void Update()
    {

        if (HasWon == false)
        {
            Vector3 CurrentRot = futureGameObject.transform.localEulerAngles;

            //local euler
            Vector3 TargetRot = new Vector3(CurrentRot.x + (-MicrogameInputManager.MouseMovementNormalized.y * Sensitivity * Time.deltaTime),
                                                                      0.0f,
                                                                      CurrentRot.z + (MicrogameInputManager.MouseMovementNormalized.x * Sensitivity * Time.deltaTime));

            if (Clamp)
            {
                if (TargetRot.x < 180.0f && TargetRot.x > ClampedRangePositive)
                {
                    TargetRot.x = ClampedRangePositive;
                }
                else if (TargetRot.x > 180.0f && TargetRot.x < ClampedRangeNegative)
                {
                    TargetRot.x = ClampedRangeNegative;
                }
            }

            futureGameObject.transform.localEulerAngles = TargetRot;
        }
    }

    public void WinFunc() 
    {
        HasWon = true;

        MicrogameHandler.WinWhenTimeIsUp();
        MicrogameHandler.CancelTimer();
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence() 
    { 
       yield return new WaitForSeconds(1.0f);
       MicrogameHandler.Win();
    }
}
