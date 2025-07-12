using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndertaleBattle
{
    public class StraightAttack : MonoBehaviour
    {
        public AnimationCurve attackCurve;
        public float distanceToTravel;
        float time;
        public float lifetime;
        Vector3 initialPos;
        public bool faceHeart;
        public GameObject despawnEffect;

        void Awake()
        {
            initialPos = transform.position;
            if (faceHeart)
            {
                Transform heart = GameObject.Find("Heart").transform;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, heart.position - transform.position);
            }
        }

        void Update()
        {
            transform.position = initialPos + (transform.up * attackCurve.Evaluate(time / lifetime) * distanceToTravel);
            time += Time.deltaTime;
            if (time >= lifetime)
            {
                Instantiate(despawnEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
