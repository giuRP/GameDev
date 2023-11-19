using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEndDetector : MonoBehaviour
{
    public LayerMask confinerMask;

    public Vector2 rayDirection;
    public float screenRaycastLength = 2;

    [Range(0, 1)]
    public float screenRaycastDelay = 0.1f;

    public bool PathBlocked { get; private set; }

    public event Action OnPathBlocked;

    [Header("Gizmos Parameters")]
    public Color screenRaycastColor = Color.blue;
    public bool showGizmos = true;

    private void Start()
    {
        StartCoroutine(CheckInsideScreenCoroutine());
    }

    IEnumerator CheckInsideScreenCoroutine()
    {
        yield return new WaitForSeconds(screenRaycastDelay);
        var hit = Physics2D.Raycast(transform.position, rayDirection, screenRaycastLength, confinerMask);

        if (hit.collider != null)
        {
            OnPathBlocked?.Invoke();
        }

        PathBlocked = hit.collider != null;

        StartCoroutine(CheckInsideScreenCoroutine());
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = screenRaycastColor;
            Gizmos.DrawRay(transform.position, rayDirection * screenRaycastLength);
        }
    }
}
