using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TandooriJeans63_MakeASandwich {
    public class ToppingSpawner : MonoBehaviour {

        [SerializeField] GameObject[] toppingPrefabs;
        [SerializeField] float spawnInterval = 1f;
        [SerializeField] float spawnXRange = 6f;
        [SerializeField] float spawnY = 6f;
        private bool isSpawning = true;
        private float timer;

        void Update() {
            if (!isSpawning) return;
            timer += Time.deltaTime;

            if (timer >= spawnInterval) {
                timer = 0;
                SpawnTopping();
            }
        }
        public void StopSpawning() {
            isSpawning = false;
        }

        void SpawnTopping() {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY, 0f);
            int index = Random.Range(0, toppingPrefabs.Length);
            Instantiate(toppingPrefabs[index], spawnPos, Quaternion.EulerRotation(-90,0,0));
        }
    }
}
