using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndertaleBattle
{
    public class HomingAttack : MonoBehaviour
    {
        public float moveSpeed;
        public float rotSpeed;
        public float rotVariance;
        Transform heart;
        public GameObject despawnEffect;
        [Header("LifeSettings")]
        public float lifetime;
        public float timeAfterDeath;

        void Awake()
        {
            heart = GameObject.Find("Heart").transform;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, heart.position - transform.position);
            rotSpeed = Random.Range(rotVariance - rotVariance, rotSpeed + rotVariance);
        }

        void Update()
        {
            if (lifetime > 0)
            {
                Vector3 heartDir = heart.position - transform.position;
                heartDir.z = 0;
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, heartDir);

                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
                lifetime -= Time.deltaTime;
            }
            else if (timeAfterDeath > 0)
            {
                timeAfterDeath -= Time.deltaTime;
                if (timeAfterDeath <= 0)
                {
                    Instantiate(despawnEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
    }
}
