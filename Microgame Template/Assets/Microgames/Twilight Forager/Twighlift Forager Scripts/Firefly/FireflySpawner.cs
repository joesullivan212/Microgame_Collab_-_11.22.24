using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    [SerializeField] List<FireflyConfigSO> fireflyConfigs;
    [SerializeField] float minTimeBetweenInstances = 1f;
    [SerializeField] float maxTimeBetweenInstances = 3f;

    FireflyConfigSO currentInstance;

    [SerializeField] GameObject fireflyPrefab;
    [SerializeField] bool isLooping = true;
    void Start()
    {
        StartCoroutine(SpawnFireflies());
    }

    public FireflyConfigSO GetCurrentInstance()
    {
        return currentInstance;
    }


    IEnumerator SpawnFireflies()
    {
        do
        {
            foreach (FireflyConfigSO firefly in fireflyConfigs)
            {
                currentInstance = firefly;
                Instantiate(fireflyPrefab, firefly.GetStartingWaypoint().position, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(minTimeBetweenInstances, maxTimeBetweenInstances));
            }
        } while (isLooping);
    }
}
