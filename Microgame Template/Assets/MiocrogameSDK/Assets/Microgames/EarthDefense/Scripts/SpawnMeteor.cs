using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject Meteor;

    public bool ShouldSpawn = true;

    public float SpawnRate = 3.0f;
    public float SpawnRateDecay = 0.8f;

    public float Radius = 45.0f;

    public Timer timer;

    public List<GameObject> ActiveMeteors =  new List<GameObject> ();

    public void Start()
    {
        StartCoroutine(Co_SpawnMeteor());
    }

    public void Update()
    {
        if(timer.remainingTime <= 0.0f)
        {
            ShouldSpawn = false;
        }
    }

    IEnumerator Co_SpawnMeteor()
    {
        while (ShouldSpawn)
        {
            Vector2 RandomVector2 = new Vector2 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 Vector2Normalized = RandomVector2.normalized;
            Vector3 SpawnVector3 = new Vector3(Vector2Normalized.x, Vector2Normalized.y, 0.0f);
            GameObject ThisMeteor =  Instantiate(Meteor, gameObject.transform.position + (SpawnVector3 * Radius), gameObject.transform.rotation, transform);
            ActiveMeteors.Add(ThisMeteor);
            yield return new WaitForSeconds(SpawnRate);
            SpawnRate *= SpawnRateDecay;
        }
    }

    public void DestroyAllMeteors()
    {
        for(int i = 0; i < ActiveMeteors.Count; i++)
        {
            if(ActiveMeteors[i] != null)
            {
                Destroy(ActiveMeteors[i]);
            }
        }
    }
}
