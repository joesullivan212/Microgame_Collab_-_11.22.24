using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndertaleBattle
{
    public class AttackRandomizer : MonoBehaviour
    {
        public List<GameObject> attacks;
        public float initialTime;
        public float minTimeBtwAttacks;
        public float maxTimeBtwAttacks;
        float time;

        void Awake()
        {
            if(initialTime == 0) time = Random.Range(minTimeBtwAttacks, maxTimeBtwAttacks);
            else time = initialTime;
        }

        void Update()
        {
            if (attacks.Count > 0)
            {
                if (time > 0) time -= Time.deltaTime;
                else
                {
                    int attack = Random.Range(0, attacks.Count - 1);
                    attacks[attack].SetActive(true);
                    attacks.RemoveAt(attack);
                    time = Random.Range(minTimeBtwAttacks, maxTimeBtwAttacks);
                }
            }
        }
    }
}
