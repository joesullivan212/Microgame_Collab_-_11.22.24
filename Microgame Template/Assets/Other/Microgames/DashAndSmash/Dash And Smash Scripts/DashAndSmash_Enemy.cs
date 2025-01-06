using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Enemy : MonoBehaviour
{
    private Transform player;
    public float speed = 3f;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private List<AudioClip> deathSounds;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    public IEnumerator DestroyEnemy()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
        }

        if (deathSounds.Count > 0)
        {
            int randomIndex = Random.Range(0, deathSounds.Count);
            AudioClip randomSound = deathSounds[randomIndex];
            AudioSource.PlayClipAtPoint(randomSound, transform.position, Random.Range(0.8f, 1.2f));
        }

        Destroy(gameObject);

        yield return null;
    }
}