using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowMouse : MonoBehaviour
{
    public Vector2 offset = new Vector2(50f, -30f); 
    public float maxYPosition = 200f; 

    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {

            Vector2 targetPosition = (Vector2)Input.mousePosition + offset;
            targetPosition.y = Mathf.Min(targetPosition.y, maxYPosition);
            rectTransform.position = targetPosition;
        }
        else
        {

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            Vector2 targetPosition = localPoint + offset;
            targetPosition.y = Mathf.Min(targetPosition.y, maxYPosition);

            rectTransform.anchoredPosition = targetPosition;
        }
    }
}
