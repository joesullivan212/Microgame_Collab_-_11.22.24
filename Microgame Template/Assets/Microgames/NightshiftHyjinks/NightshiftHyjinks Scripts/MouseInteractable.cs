using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nightshift
{
    public class MouseInteractable : MonoBehaviour
    {
        public UnityEvent onClick;
        
        void OnMouseDown()
        {
            onClick?.Invoke();
        }
    }
}
