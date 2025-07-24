using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepCounterUI : MonoBehaviour
{
    [Header("Necessary Components")]
    public AbRollerMiniGame abRollerminiGame;
    public AbRollerCharacter abRollercharacter;
    public TextMeshProUGUI repText;

    void UpdateRepUI() => repText.text = abRollerminiGame.Reps.ToString();

    public void OnEnable()
    {
        abRollercharacter.OnRep.AddListener(UpdateRepUI);
    }

    private void OnDisable()
    {
        abRollercharacter.OnRep.RemoveListener(UpdateRepUI);
    }
}
