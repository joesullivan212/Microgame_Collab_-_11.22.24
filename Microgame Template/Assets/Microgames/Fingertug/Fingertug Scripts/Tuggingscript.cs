using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuggingscript : MonoBehaviour
{
    public int tugamount;
    public MicrogameHandler MicroGameHandler;
    public GameObject tugman;
    public Material tugmaterial;
    public Material idlematerial;
    public Material winmaterial;
    public Material losematerial;
    private bool youwin;
    private bool youlose;
    public float timer;
    public bool iswin;

    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            if (youwin == true)
            {
                return;
            }

            if (youlose == true)
            {
                return;
            }

            else
            {
                StartCoroutine(delay());
            }
        }

        if (tugamount == 35)
        {
            StartCoroutine(Win());
        }

        if (timer <= 0)
        {
            if (iswin == true)
            {
                return;
            }
            youlose = true;
            tugman.GetComponent<MeshRenderer>().material = losematerial;
        }
    }

    IEnumerator delay()
    {
        tugman.GetComponent<MeshRenderer>().material = tugmaterial;
        tugamount += 1;
        yield return new WaitForSeconds(1);
        tugman.GetComponent<MeshRenderer>().material = idlematerial;
    }

    IEnumerator Win()
    {
        iswin = true;
        youwin = true;
        tugman.GetComponent<MeshRenderer>().material = winmaterial;
        yield return new WaitForSeconds(1);
        MicroGameHandler.Win();
    }
}
