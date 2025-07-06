using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class NotesManager : MonoBehaviour
{

    public string[] notes;
    public TextMeshProUGUI[] notesTextBoxes;

    public TextMeshProUGUI bigNote;

    public GameObject Scene01;
    public GameObject Scene02;

    public TextMeshProUGUI pinOutPut;

    public string enteredPin;

    public string correctCode;

    public SubtleCameraFollow cf;

    public MicrogameHandler microgameHandler;

    public Timer timer;

    public AudioSource gun;
    public AudioSource win;
    public AudioSource musicPlayer;

    bool endGame;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < notesTextBoxes.Length; i++)
        {
            notesTextBoxes[i].text = notes[Random.Range(0, notes.Length)];
        }


        int j = Random.Range(0, notesTextBoxes.Length);
        int PIN = Random.Range(1000, 9999);
        correctCode = PIN.ToString();
        notesTextBoxes[j].text = "PIN: " + PIN.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame) return;
        if(timer.remainingTime < 0.5f)
        {
           
            Death();
        }
    }

    public void NoteClicked(GameObject note)
    {
        cf.keepStill = true;
        Scene01.SetActive(false);
        Scene02.SetActive(true);
        Cursor.visible = false;


        bigNote.text = note.GetComponent<TextMeshProUGUI>().text;
    }

    public void RetrunToDesk()
    {
        Scene01.SetActive(true);
        Scene02.SetActive(false);
        cf.keepStill = false;
        Cursor.visible = true;
    }


    bool end;
    public void EnterNumber(int number)
    {
      
        if(endGame) return;
        if (end) return;
        enteredPin += number.ToString();
        pinOutPut.text += number.ToString();

        if (enteredPin.Length >= 4)
        {
            Debug.Log("END");

            if(enteredPin == correctCode)
            {
                Hacked();
                
            }
            else Death();

            end = true;
        }

        


    }

    public void DeleteNumber()
    {
        if (end) return;
        if (enteredPin.Length <= 0) return;

        enteredPin = enteredPin.Substring(0, enteredPin.Length - 1);
        pinOutPut.text = pinOutPut.text.Substring(0, pinOutPut.text.Length - 1);
    }

    void Death()
    {
        endGame = true;
        microgameHandler.CancelTimer();
        Scene01.SetActive(false);
        Scene02.SetActive(false);
        gun.Play();
        musicPlayer.Stop();
        StartCoroutine(LooseGameForReal());
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Hacked()
    {
        endGame = true;
        Scene01.SetActive(false);
        Scene02.SetActive(false);
        musicPlayer.Stop();
        microgameHandler.PauseTimer();
        StartCoroutine(WinGameForReal());
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        win.Play();
    }

    IEnumerator LooseGameForReal()
    {

        yield return new WaitForSeconds(2);

        microgameHandler.Lose();
    }

    IEnumerator WinGameForReal()
    {

        yield return new WaitForSeconds(2.7f);

        microgameHandler.Win();
    }

}
