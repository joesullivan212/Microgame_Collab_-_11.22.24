using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class PostGenerator : MonoBehaviour
    {
        public GameObject post;
        public Scroll scroll;
        public float offset;
        public float randomness;

        void Awake()
        {
            for (float pos = 0; pos < scroll.bounds.y + offset; pos += offset)
            {
                GameObject child = Instantiate(post, new Vector3(Random.Range(-randomness, randomness), -pos, 0), Quaternion.identity);
                child.transform.parent = transform;
            }
        }
    }
}
