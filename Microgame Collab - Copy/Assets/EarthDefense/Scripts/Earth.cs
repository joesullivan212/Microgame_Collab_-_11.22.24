using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Timer timer;
    public MicrogameHandler microgameHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Meteor>(out Meteor meteor))
        {
            return;
        }

        Death();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Meteor>(out Meteor meteor))
        {
            return;
        }

        Death();
    }

    private void Death()
    {
        sprite.color = Color.red;

        timer.CancelTimer();

        Invoke(nameof(LoseGame), 2.0f);
    }

    public void LoseGame()
    {
        Debug.Log("Lose");
        microgameHandler.Lose();
    }
}
