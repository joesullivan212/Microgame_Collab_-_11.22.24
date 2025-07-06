using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperateCells : MonoBehaviour
{
    public Transform[] Cells;

    public float min, max;

    public void SeperateCellsFunc()
    {
        foreach (var cell in Cells)
        {
            float Offset = Random.Range(min, max);
            cell.position = new Vector3(cell.position.x, cell.position.y + Offset, cell.position.z);
        }
    }
}
