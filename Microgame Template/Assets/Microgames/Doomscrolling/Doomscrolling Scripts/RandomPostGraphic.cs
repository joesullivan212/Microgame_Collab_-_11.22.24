using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doomscrolling
{
    public class RandomPostGraphic : MonoBehaviour
    {
        public Sprite[] posts;

        void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = posts[Random.Range(0, posts.Length)];
        }
    }
}
