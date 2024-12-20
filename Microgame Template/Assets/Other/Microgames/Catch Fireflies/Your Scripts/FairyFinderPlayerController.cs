using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyFinderPlayerController : MonoBehaviour
{
    public Camera camera;
    public MicrogameInputManager microgameInputManager;
    public MicrogameHandler microgameHandler;

    public GameObject FairyBurst;

    public GameObject GiantFairyBurst;
    public GameObject MassiveFairyBurst;

    public GameObject DirectionalLight;
    public GameObject TextGameObject;

    private int FairiesCaught;
    public int FariesNeeded;
    public bool WonGame = false;

    public float FireworksDelay;
    public float TimeBetweenFireworksMin, TimeBetweenFireworksMax;
    public float TimeBeforeMoonFireWork;
    public float MoonExplosionDuration;
    public GameObject[] Stars;
    public GameObject Moon;

    public GameObject FinalScoreText;
    public float WinLoseJudgementTime = 8.0f;


    private void Start()
    {
        Invoke("CallALoss", WinLoseJudgementTime);
    }

    // Start is called before the first frame update
    void Update()
    {
        if (microgameInputManager.Clicked)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                Debug.Log("Hit : " + objectHit.name);

                if (objectHit.CompareTag("Enemy")) 
                {
                    FairiesCaught++;

                    UpdateFinalScoreText();

                    Instantiate(FairyBurst, objectHit.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                    GameObject TextObject = Instantiate(TextGameObject, objectHit.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));

                    TextObject.GetComponent<TextMeshPro>().text = FairiesCaught.ToString() + "/" + FariesNeeded.ToString();

                    Destroy(objectHit.gameObject);

                    if(FairiesCaught >= FariesNeeded) 
                    {
                        OnWin();
                    }
                   
                }
                if (objectHit.CompareTag("Environment")) 
                {
                    Instantiate(GiantFairyBurst, objectHit.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    DirectionalLight.SetActive(false);
                    Destroy(objectHit.gameObject);
                }
            }
        }
    }

    public void OnWin() 
    {
        microgameHandler.WinWhenTimeIsUp();
        WonGame = true;
        StartCoroutine("SetOffFireWorks");
        microgameHandler.CancelTimer();
    }

    public void OnLose() 
    {
        FinalScoreText.SetActive(true);
        UpdateFinalScoreText();
    }

    public void UpdateFinalScoreText() 
    {
        FinalScoreText.GetComponent<TextMeshPro>().text = FairiesCaught.ToString() + "/" + FariesNeeded.ToString();
    }

    public void CallALoss() 
    {
        if (WonGame == false) 
        {
            OnLose();
        }
    }

    IEnumerator SetOffFireWorks() 
    {
        yield return new WaitForSeconds(FireworksDelay);

        if (WonGame) 
        { 
          foreach(GameObject Star in Stars) 
          {
                if (Star != null)
                {
                    float TimeBetween = Random.Range(TimeBetweenFireworksMin, TimeBetweenFireworksMax);
                    yield return new WaitForSeconds(TimeBetween);
                    Instantiate(GiantFairyBurst, Star.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Destroy(Star);
                }
          }

            yield return new WaitForSeconds(TimeBeforeMoonFireWork);
            Instantiate(MassiveFairyBurst, Moon.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            Destroy(Moon);

            yield return new WaitForSeconds(MoonExplosionDuration);

            microgameHandler.Win();
        }


    }
}
