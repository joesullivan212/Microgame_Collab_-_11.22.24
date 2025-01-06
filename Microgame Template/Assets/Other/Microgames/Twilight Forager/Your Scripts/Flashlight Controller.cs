using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Light spotLight;
    [SerializeField] float baseIntensity = 30f;
    [SerializeField] float baseLightAngle = 40f;
    [SerializeField] float bookIntensity = 5f;
    [SerializeField] float bookLightAngle = 100f;
    [SerializeField] float maxDistance = 10f;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            transform.LookAt(hit.point);
            if (hit.collider.gameObject.CompareTag("Book"))
            {
                spotLight.intensity = bookIntensity;
                spotLight.spotAngle = bookLightAngle;
            }
            else
            {
                spotLight.intensity = baseIntensity;
                spotLight.spotAngle = baseLightAngle;
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
    }
}
