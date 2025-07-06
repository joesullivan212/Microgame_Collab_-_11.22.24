using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorSprite; 
    public Vector2 hotspot = Vector2.zero; 

    void Start()
    {
        SetCursor();
    }

    void SetCursor()
    {
        if (cursorSprite != null)
        {
            Cursor.SetCursor(cursorSprite, hotspot, CursorMode.Auto);
        }
        else
        {
            Debug.LogWarning("Cursor sprite is not assigned!");
        }
    }
}
