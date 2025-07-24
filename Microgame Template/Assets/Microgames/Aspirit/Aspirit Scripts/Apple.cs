using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SnakeController snake = other.gameObject.GetComponent<SnakeController>();

        if(snake != null)
        {
            snake.GrowSnake();
            Destroy(gameObject);
        }
    }
}
