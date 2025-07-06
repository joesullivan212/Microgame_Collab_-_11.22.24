using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TandooriJeans63_MakeASandwich {
    public class PlayerController : MonoBehaviour {
        public MicrogameInputManager microgameInputManager;

        [SerializeField]float speed = 5f;
        void Update() {
            if (microgameInputManager.ArrowKeysDirection.x > 0) {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (microgameInputManager.ArrowKeysDirection.x < 0) {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -6f, 6f);
            transform.position = clampedPosition;
        }
        
    }
}
