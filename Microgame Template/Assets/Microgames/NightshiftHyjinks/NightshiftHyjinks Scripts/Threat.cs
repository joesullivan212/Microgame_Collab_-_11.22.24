using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightshift
{
    public class Threat : MonoBehaviour
    {
        public MicrogameHandler handler;
        public CameraView cameraView;
        public GameObject[] reportedParticles;

        int reported;

        AudioSource audioS;

        void Awake()
        {
            int firstChild = Random.Range(0, transform.childCount);
            transform.GetChild(firstChild).gameObject.SetActive(true);

            int secondChild = Random.Range(0, transform.childCount);
            secondChild = secondChild != firstChild ? secondChild : secondChild > 0 ? secondChild - 1 : secondChild + 1;
            transform.GetChild(secondChild).gameObject.SetActive(true);

            audioS = GetComponent<AudioSource>();
        }

        public void Reported(GameObject burglar)
        {
            audioS.Play();

            burglar.SetActive(false);
            Instantiate(reportedParticles[reported], burglar.transform.position, Quaternion.identity);
            cameraView.TryStatic(cameraView.viewID);
            reported++;
            if (reported == 2) handler.WinWhenTimeIsUp();
        }
    }
}
