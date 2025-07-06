using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] float blinkDuration = 2f;
    FireflySpawner fireflySpawner;
    FireflyConfigSO fireflyConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    Material fireflyMaterial;

    float blinkTimeTracker;
    float rotationSpeed = 100f;

    void Awake()
    {
        fireflySpawner = FindObjectOfType<FireflySpawner>();
        fireflyMaterial = GetComponent<MeshRenderer>().material;
    }
    void Start()
    {
        fireflyConfig = fireflySpawner.GetCurrentInstance();
        waypoints = fireflyConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
        Blink();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float moveSpeed = fireflyConfig.GetMoveSpeed();

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Blink()
    {
        if (fireflyMaterial != null)
        {
            blinkTimeTracker += Time.deltaTime;
            float lerpFactor = Mathf.PingPong(blinkTimeTracker / blinkDuration, 1.0f);
            Color emissionColor = Color.Lerp(color1, color2, lerpFactor);

            fireflyMaterial.SetColor("_EmissionColor", emissionColor);

            DynamicGI.SetEmissive(GetComponent<Renderer>(), emissionColor);
        }
    }
}
