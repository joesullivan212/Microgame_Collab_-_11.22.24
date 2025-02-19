using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace KillTheGerms
{
    public class Sponge : MonoBehaviour
    {
        [SerializeField] Camera cam;

        public MicrogameInputManager microgameInputManager;

        private void Update()
        {
            var pos = cam.ScreenToWorldPoint(microgameInputManager.MouseScreenPosition);

            pos.z = 0;
            transform.position = pos;
        }
    }
}