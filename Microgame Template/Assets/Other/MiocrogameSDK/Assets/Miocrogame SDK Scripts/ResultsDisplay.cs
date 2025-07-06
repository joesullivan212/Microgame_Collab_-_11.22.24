using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsDisplay : MonoBehaviour
{
    [SerializeField] GameObject WinIcon;
    [SerializeField] GameObject LoseIcon;

    public float DelayBeforeInstantiating = 1.0f;

    public List<GameObject> ActiveIcons = new List<GameObject>();

    public void Refresh() 
    {
        ClearIcons();

        int Index = 0;

        foreach (Result result in AreaManager.instance.Results)
        {
            //See if it is the newest result
            if (Index == AreaManager.instance.Results.Count)
            {
                AddIconWithDelay(result);
                break;
            }
            else
            {
                AddIcon(result);
            }

            Index++;
        }

    }

    public void AddIcon(Result result) 
    {
        if (result.Won)
        {
            AddWinIcon();
        }
        else
        {
            AddLoseIcon();
        }
    }

    IEnumerator AddIconWithDelay(Result result)
    {
        yield return new WaitForSeconds(DelayBeforeInstantiating);

        if (result.Won)
        {
            AddWinIcon();
        }
        else
        {
            AddLoseIcon();
        }
    }

    public void AddWinIcon()
    {
        GameObject NewIcon = Instantiate(WinIcon, transform);

        ActiveIcons.Add(NewIcon);
    }

    public void AddLoseIcon() 
    {
        GameObject NewIcon = Instantiate(LoseIcon, transform);

        ActiveIcons.Add(NewIcon);
    }

    public void ClearIcons() 
    { 
       foreach(GameObject Icon in ActiveIcons) 
        {
            Destroy(Icon);
        }

        ActiveIcons.Clear();
    }
}
