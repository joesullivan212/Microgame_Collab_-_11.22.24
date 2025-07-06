using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndSmash_Dash : MonoBehaviour
{
    [SerializeField] private MicrogameHandler microgameHandler;
    [SerializeField] private MicrogameInputManager microGameInput;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private float dashTimeLeft;
    private Vector3 dashDirection;

    void Start()
    {
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        microgameHandler.WinWhenTimeIsUp();
    }

    void Update()
    {
        if (!isDashing)
        {
            if (microGameInput.Clicked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
                {
                    StartDash(hit.point);
                }
            }
        }
        else
        {
            Dash();
        }
    }

    void StartDash(Vector3 targetPosition)
    {
        isDashing = true;
        dashTimeLeft = dashDuration;

        targetPosition.y = transform.position.y;
        dashDirection = (targetPosition - transform.position).normalized;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }
    }

    void Dash()
    {
        if (dashTimeLeft > 0)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime, Space.World);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            if (trailRenderer != null)
            {
                trailRenderer.emitting = false;
            }
        }
    }
}