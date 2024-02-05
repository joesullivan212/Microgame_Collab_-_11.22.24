using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject EarthExplosionGameObject;
    public Timer timer;
    public MicrogameHandler microgameHandler;
    public SpawnMeteor spawnMeteor;
    public GameObject WorldSheild;

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
        sprite.color = Color.clear;

        EarthExplosionGameObject.SetActive(true);

        spawnMeteor.DestroyAllMeteors();

        spawnMeteor.ShouldSpawn = false;

        WorldSheild.SetActive(false);

        timer.CancelTimer();

        Invoke(nameof(LoseGame), 2.0f);
    }

    public void LoseGame()
    {
        Debug.Log("Lose");
        microgameHandler.Lose();
    }
}
