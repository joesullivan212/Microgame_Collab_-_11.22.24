using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseClamper : MonoBehaviour
{
    public VirtualMouseInput virtualMouseInput;

    public RectTransform CanvasRect;

    private void Update()
    {
        //transform.localScale = Vector3.one * (1.0f / CanvasRect.localScale.x);
        //transform.SetAsLastSibling();
    }

    private void LateUpdate()
    {
        Vector2 pos = virtualMouseInput.virtualMouse.position.value;
        pos.x = Mathf.Clamp(pos.x, 0.0f, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0.0f, Screen.height);

        InputState.Change(virtualMouseInput.virtualMouse.position, pos);
    }
}
