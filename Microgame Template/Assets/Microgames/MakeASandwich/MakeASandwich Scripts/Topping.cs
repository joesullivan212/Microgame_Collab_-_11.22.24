using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TandooriJeans63_MakeASandwich {

    public class Topping : MonoBehaviour {
        [SerializeField] bool isCorrect;
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                GameController controller = FindObjectOfType<GameController>();
                if (isCorrect) {
                    controller.CaughtTopping(gameObject);
                }
                else {
                    controller.CaughtWrongTopping();
                    Destroy(gameObject);
                }
            }
            else if (other.CompareTag("Enemy")) {
                Destroy(gameObject);
            }
        }
    }
}
