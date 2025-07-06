using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBusinessButtonLogic : MonoBehaviour
{
    public int index;
    public int buySellIndex;
    public MonkeyBusinessGameManager gameManager;
    
    public void ButtonSelected()
    {
        gameManager.Guess(index, buySellIndex);
    }
}
