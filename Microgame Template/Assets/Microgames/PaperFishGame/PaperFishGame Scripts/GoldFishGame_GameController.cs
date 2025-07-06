using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGame_GameController : MonoBehaviour
{
    public MicrogameHandler microgameHandler;

    public int HoopsScored;

    public int HoopsNeededToWin;

    public bool HasWon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasWon == false)
        {
            if (HoopsScored >= HoopsNeededToWin)
            {
                microgameHandler.WinWhenTimeIsUp();
                HasWon = true;
            }
        }
    }
}
