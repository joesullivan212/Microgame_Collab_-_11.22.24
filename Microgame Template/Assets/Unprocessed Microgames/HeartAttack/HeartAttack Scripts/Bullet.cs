using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UndertaleBattle
{
    public class Bullet : MonoBehaviour
    {
        public GameObject spawnEffect;
        public bool useParentForSpawnEffect;
        public bool destroyOnHit;

        void OnEnable()
        {
            Vector3 pos = useParentForSpawnEffect ? transform.parent.position : transform.position;
            Instantiate(spawnEffect, pos, Quaternion.identity);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.name == "Heart")
            {
                if(other.gameObject.GetComponent<HeartManager>().Hit() && destroyOnHit) Destroy(gameObject);
            }
        }
    }
}
