using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TandooriJeans63_MakeASandwich {

    public class GameController : MonoBehaviour {

        [SerializeField] Transform plate;
        [SerializeField] GameObject topBunPrefab;
        private int correctCaught = 0;
        [SerializeField] int requiredToppings = 5;
        [SerializeField] float stackHeight = 0.5f;
        private int missed = 0;
        [SerializeField] MicrogameHandler MicrogameHandler;
        [SerializeField] ToppingSpawner toppingSpawner;
        private bool gameEnded = false;

        public void CaughtTopping(GameObject topping) {
            if (gameEnded) return;
            topping.transform.SetParent(plate);
            correctCaught++;

            topping.transform.position = new Vector3(plate.transform.position.x, plate.transform.position.y +(correctCaught * stackHeight), plate.transform.position.z);
            topping.GetComponent<Rigidbody>().isKinematic = true;
            topping.GetComponent<Collider>().enabled = false;

            if (correctCaught >= requiredToppings) {
                Invoke(nameof(PlaceTopBunAndWin), 0.5f);
            }
        }
        void PlaceTopBunAndWin() {
            topBunPrefab.SetActive(true);
            topBunPrefab.transform.SetParent(plate);
            topBunPrefab.transform.position = new Vector3(plate.transform.position.x, plate.transform.position.y + (correctCaught * stackHeight), plate.transform.position.z);
            MicrogameHandler.Win();
            Win();  
        }
        public void CaughtWrongTopping() {
            Lose();
            MicrogameHandler.Lose();

        }
        public void Win() {
            if (gameEnded) return;
            Stop();
        }

        public void Lose() {
            if (gameEnded) return;
            Stop();
            MicrogameHandler.Lose();
        }
        private void Stop() {
            gameEnded = true;
            toppingSpawner.StopSpawning();
        }
        
    }
}
