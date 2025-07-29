using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Nightshift
{
    public class CameraView : MonoBehaviour
    {
        public int viewID;
        public StaticAlpha staticA;
        AudioSource audioS;

        void Awake()
        {
            audioS = GetComponent<AudioSource>();
        }

        public void ChangeView(int ID)
        {
            audioS.Play();
            TryStatic(viewID);
            if (ID != viewID)
            {
                viewID = ID;
                transform.position = new Vector3(0, viewID * 10, -10);
            }
        }

        public void TryStatic(int ID)
        {
            if(ID == viewID)
            {
                staticA.SetAlpha(1);
            }
        }
    }
}
