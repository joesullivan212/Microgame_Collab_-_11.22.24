using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightShift_LevelPouch : MonoBehaviour
{
    public int LevelIndex;

    public bool IsSelected;

    public Transform TargetSelectedTransform;

    public Vector3 OriginalPosition;
    public Vector3 OriginalRotation;

    public float PositionLerpSpeed;
    public float RotationLerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPos = new Vector3();
        Vector3 TargetRotation = new Vector3();

        if (IsSelected) 
        {
            TargetPos = TargetSelectedTransform.position;
            TargetRotation = TargetSelectedTransform.rotation.eulerAngles;
        }
        else 
        {
            TargetPos = OriginalPosition;
            TargetRotation = OriginalRotation;
        }

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, TargetPos, PositionLerpSpeed * Time.deltaTime);
        gameObject.transform.eulerAngles = new Vector3(Mathf.LerpAngle(gameObject.transform.rotation.eulerAngles.x, TargetRotation.x, RotationLerpSpeed * Time.deltaTime),
                                                     Mathf.LerpAngle(gameObject.transform.rotation.eulerAngles.y, TargetRotation.y, RotationLerpSpeed * Time.deltaTime),
                                                     Mathf.LerpAngle(gameObject.transform.rotation.eulerAngles.z, TargetRotation.z, RotationLerpSpeed * Time.deltaTime));

    }
}
