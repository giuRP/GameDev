using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    public class BeamBullet : Bullet
    {
        public override void PerformAttack()
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, movementDirection, data.range, hittableLayerMask);

            if (hit.collider != null)
            {
                foreach (var hittable in hit.collider.GetComponents<IHittable>())
                {
                    hittable.GetHit(gameObject, data.damage);
                }
                Destroy(gameObject);
            }
        }
    }
}