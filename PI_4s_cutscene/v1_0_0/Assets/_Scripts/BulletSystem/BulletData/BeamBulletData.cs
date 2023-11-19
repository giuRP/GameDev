using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    [CreateAssetMenu(fileName = "Beam Bullet Data", menuName = "Bullets/BeamBullets")]
    public class BeamBulletData : _BulletData
    {
        GameObject throwable;

        public override void StartBullet(AgentWeaponManager weapon)
        {
            throwable = Instantiate(prefab, weapon.transform.position, Quaternion.identity);

            throwable.GetComponent<Throwable>().DisableBullet();
        }

        public override void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction)
        {
            throwable.GetComponent<Throwable>().Initialize(this, hittableMask, direction);
            throwable.GetComponent<Throwable>().EnableBullet();
        }

        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            Gizmos.DrawLine(origin, origin + direction * range);
        }   
    }
}