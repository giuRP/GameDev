using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AISystem
{
    public class AIMeleeAttackDetector : MonoBehaviour
    {
        public LayerMask targetMask;

        public UnityEvent<GameObject> OnPlayerDetected;

        [Range(.1f, 1)]
        public float detectorRadius;

        public bool PlayerDetected { get; private set; }

        [Header("Gizmos Parameters")]
        public Color gizmoColor = Color.green;
        public bool showGizmos = true;

        private void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, detectorRadius, targetMask);
            PlayerDetected = collider != null;

            if (PlayerDetected)
                OnPlayerDetected?.Invoke(collider.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawSphere(transform.position, detectorRadius);
            }
        }
    }
}