using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.RespawnSystem
{
    public class RespawnPoint : MonoBehaviour
    {
        public UnityEvent OnSpawnActivated { get; set; }

        GameObject targetToRespawn;

        [Header("Gizmo Parameters")]
        public Color gizmosColor = Color.red;
        public bool showGizmo = true;

        public void SetTarget(GameObject objectToSpawn)
        {
            targetToRespawn = objectToSpawn;
        }

        public void RespawnTarget()
        {
            Instantiate(targetToRespawn, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmos()
        {
            if (showGizmo)
            {
                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(transform.position, 1f);
            }
        }
    }
}
