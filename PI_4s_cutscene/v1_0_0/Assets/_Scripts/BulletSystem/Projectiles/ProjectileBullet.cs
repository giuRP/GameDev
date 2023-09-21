using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    public class ProjectileBullet : Bullet
    {
        [Header("Collision Detection Data")]
        [SerializeField]
        private Vector2 center = Vector2.zero;
        [SerializeField]
        [Range(0.1f, 2f)]
        private float radius = 1;
        [SerializeField]
        private Color gizmoColor = Color.red;

        public override void PerformAttack()
        {
            Collider2D collision = Physics2D.OverlapCircle((Vector2)transform.position + center, radius, hittableLayerMask);
            if (collision != null)
            {
                foreach (var hittable in collision.GetComponents<IHittable>())
                {
                    hittable.GetHit(gameObject, data.damage);
                }
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position + (Vector3)center, radius);
        }
    }
}