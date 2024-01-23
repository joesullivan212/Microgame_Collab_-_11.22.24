using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KillTheGerms
{
    public class GermFace : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem ps;

        bool isAlive = false;
        public static event Action<ParticleSystem> OnPlayParticleSystem;
        public static event Action<ParticleSystem> OnIsDead;

        private void Update()
        {
            if (!isAlive)
                return;

            if (!ps.IsAlive())
            {
                isAlive = false;
                OnIsDead?.Invoke(ps);
            }
        }

        //called by animator
        public void Sneeze()
        {
            ps.Play();
            OnPlayParticleSystem?.Invoke(ps);
            isAlive = true;
        }
    }
}