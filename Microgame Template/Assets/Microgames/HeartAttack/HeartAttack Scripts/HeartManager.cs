using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UndertaleBattle
{
    public class HeartManager : MonoBehaviour
    {
        public MicrogameInputManager inputManager;
        public int health;
        public float invulnerableTime;
        float invulnerable;
        public float flashSpeed;
        float flashTime;
        public float moveSpeed;
        public float lookRadius;
        public Sprite[] mouthSprites;
        Rigidbody2D rb;
        SpriteRenderer rend;
        SpriteRenderer eyesRend;
        SpriteRenderer pupilsRend;
        SpriteRenderer mouthRend;
        GameObject sweat;
        public float eyeJitterStrength;
        float jitterOffset;
        public GameObject hitEffect;
        public GameObject deathEffect;
        public HealthDisplay healthDisplay;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rend = GetComponent<SpriteRenderer>();
            eyesRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
            pupilsRend = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            mouthRend = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
            sweat = transform.GetChild(1).gameObject;
        }

        void FixedUpdate()
        {
            rb.velocity = inputManager.ArrowKeysDirection * moveSpeed;
            if (invulnerable > 0)
            {
                flashTime += Time.fixedDeltaTime;
                if (flashTime >= flashSpeed)
                {
                    SetSpriteColors(rend.color.a == 1 ? new Color(1, 1, 1, 0) : Color.red);
                    flashTime = 0;
                }

                invulnerable -= Time.fixedDeltaTime;
                if (invulnerable <= 0)
                {
                    invulnerable = 0;
                    SetSpriteColors(Color.white);
                    flashTime = 0;
                    sweat.SetActive(true);
                }
            }

            // Eyes Position
            transform.GetChild(0).localPosition = new Vector3(Mathf.RoundToInt(inputManager.ArrowKeysDirection.x) * 0.0625f, Mathf.RoundToInt(inputManager.ArrowKeysDirection.y) * 0.0625f, 0);

            // Pupils Position
            jitterOffset = Random.Range(-eyeJitterStrength, eyeJitterStrength);
            Bullet[] bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
            Vector2 lookDir = new Vector2(jitterOffset, 0);
            float closestBullet = lookRadius;
            foreach (Bullet bullet in bullets)
            {
                float dist = Vector2.Distance(transform.position, bullet.transform.position);
                if (dist < closestBullet)
                {
                    float dirX = bullet.transform.position.x - transform.position.x > 0.25f ? 0.0625f : bullet.transform.position.x - transform.position.x < -0.25f ? -0.0625f : 0;
                    float dirY = bullet.transform.position.y - transform.position.y > 0.25f ? 0.0625f : bullet.transform.position.y - transform.position.y < -0.25f ? -0.0625f : 0;
                    lookDir = new Vector2(dirX + jitterOffset, dirY);
                    closestBullet = dist;
                }
            }
            transform.GetChild(0).GetChild(0).localPosition = lookDir;

            // Mouth Sprite
            mouthRend.sprite = mouthSprites[(int)(closestBullet / lookRadius * (mouthSprites.Length - 1))];
        }

        void OnDisable()
        {
            rb.velocity = Vector2.zero;
        }

        public bool Hit()
        {
            if (invulnerable == 0)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
                health -= 1;
                if (health <= 0)
                {
                    GameObject.Find("LoseManager").GetComponent<LoseManager>().Lose();
                    Instantiate(deathEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    SetSpriteColors(Color.red);
                    healthDisplay.UpdateDisplay(health);
                }

                invulnerable = invulnerableTime;
                sweat.SetActive(false);
                return true;
            }
            else return false;
        }

        void SetSpriteColors(Color col)
        {
            rend.color = col;
            eyesRend.color = col;
            pupilsRend.color = col;
            mouthRend.color = col;
        }
    }
}
