using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonkeyBusinessGameManager : MonoBehaviour
{
    public MicrogameHandler gameHandler;
    private int chosenIndex;
    private int buyOrSellIndex;
    //public TMP_Text message;
    public GameObject[] screens;
    public int[] lastIndex;
    private int count;
    public GameObject[] images;
    public GameObject[] phases;
    [SerializeField]
    private float firstOneDelay = 1f;
    [SerializeField]
    private float othersDelay = 1f;
    [SerializeField]
    private int numNeededForWin = 3;
    [SerializeField]
    private Image numCorrectVisualizationSR;
    [SerializeField]
    private Sprite[] numCorrectVisualizationSprites;

    public GameObject correctSFX;
    public GameObject incorrectSFX;

    private int buyAmount;
    private int sellAmount;

    public GameObject hitFX;
    public bool disableClick;

    void Start()
    {
        lastIndex[0] = -1;
        lastIndex[1] = -1;
        lastIndex[2] = -1;
        lastIndex[3] = -1;

        chosenIndex = Random.Range(0, 9);
        buyOrSellIndex = Random.Range(0, 2);

        Invoke("StartPhase", firstOneDelay);
    }

    public void Guess(int index, int index2)
    {
        if (count <= numNeededForWin - 1 && disableClick == false)
        {
            if (chosenIndex != index || buyOrSellIndex != index2)
            {
                //Debug.Log(chosenIndex + " " + index + " " + buyOrSellIndex + " " + index2);

                Instantiate(incorrectSFX, transform.position, transform.rotation);
                /*
                gameHandler.PauseTimer();
                Invoke("Lose", 1f);*/

                hitFX.SetActive(true);
                disableClick = true;
                Invoke("DisableHitFX", .5f);
            }

            if (chosenIndex == index && buyOrSellIndex == index2 && count >= numNeededForWin - 1)
            {
                Instantiate(correctSFX, transform.position, transform.rotation);

                gameHandler.PauseTimer();
                Invoke("Win", 1f);

                //message.text = "";

                screens[0].SetActive(true);
                screens[1].SetActive(false);
                screens[2].SetActive(false);

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].SetActive(false);
                }

                phases[0].SetActive(true);
                phases[1].SetActive(false);
                numCorrectVisualizationSR.sprite = numCorrectVisualizationSprites[numCorrectVisualizationSprites.Length - 1];
                count++;
            }

            if (chosenIndex == index && buyOrSellIndex == index2 && count < numNeededForWin - 1)
            {
                Instantiate(correctSFX, transform.position, transform.rotation);

                lastIndex[count] = chosenIndex;

                while (chosenIndex == lastIndex[0] || chosenIndex == lastIndex[1] || chosenIndex == lastIndex[2] || chosenIndex == lastIndex[3])
                {
                    chosenIndex = Random.Range(0, 9);
                }

                buyOrSellIndex = Random.Range(0, 2);

                if (buyAmount >= 4)
                {
                    buyOrSellIndex = 1;
                }
                else if (sellAmount >= 4)
                {
                    buyOrSellIndex = 0;
                }

                count++;

                numCorrectVisualizationSR.sprite = numCorrectVisualizationSprites[count];

                phases[0].SetActive(true);
                phases[1].SetActive(false);

                //message.text = "";

                screens[0].SetActive(true);
                screens[1].SetActive(false);
                screens[2].SetActive(false);

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].SetActive(false);
                }

                Invoke("StartPhase", othersDelay);
            }
        }
    }

    public void StartPhase()
    {
        phases[0].SetActive(false);
        phases[1].SetActive(true);

        if (buyOrSellIndex == 0)
        {
            //message.text = "BUY";

            screens[0].SetActive(false);
            screens[1].SetActive(true);
            screens[2].SetActive(false);

            buyAmount++;
        }
        else if (buyOrSellIndex == 1)
        {
            //message.text = "SELL";

            screens[0].SetActive(false);
            screens[1].SetActive(false);
            screens[2].SetActive(true);

            sellAmount++;
        }

        for(int i = 0; i < images.Length; i++)
        {
            if(i == chosenIndex)
            {
                images[i].SetActive(true);
            }
            else
            {
                images[i].SetActive(false);
            }
        }
    }

    public void Win()
    {
        gameHandler.Win();
    }

    public void Lose()
    {
        gameHandler.Lose();
    }

    void DisableHitFX()
    {
        hitFX.SetActive(false);
        disableClick = false;
    }
}
