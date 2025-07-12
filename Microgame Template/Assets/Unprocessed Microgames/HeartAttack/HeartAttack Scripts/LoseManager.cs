using System.Collections;
using System.Collections.Generic;
using UndertaleBattle;
using Unity.Mathematics;
using UnityEngine;

public class LoseManager : MonoBehaviour
{
    [Header("SceneFreezeComponents")]
    public MicrogameHandler handler;
    public CameraZoom cam;
    public GameObject scene;
    public GameObject heartExplosion;
    [Header("CoroutineSettings")]
    public float waitTime;

    public void Lose()
    {
        handler.CancelTimer();
        cam.enabled = false;
        scene.SetActive(false);
        StartCoroutine(LostCoroutine(GameObject.Find("Heart").transform.position));
    }

    IEnumerator LostCoroutine(Vector3 heartPos)
    {
        yield return new WaitForSeconds(1);
        Instantiate(heartExplosion, heartPos, Quaternion.identity);
        yield return new WaitForSeconds(waitTime - 1);
        handler.Lose();
    }
}
