using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    [CreateAssetMenu(fileName = "Projectile Bullet Data", menuName = "Bullets/ProjectileBullets")]
    public class ProjectileBulletData : _BulletData
    {
        public override void StartBullet(AgentWeaponManager weapon)
        {
            
        }

        public override void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction)
        {            
            GameObject throwable = Instantiate(prefab, agent.weapon.transform.position, Quaternion.identity);

            throwable.GetComponent<Throwable>().Initialize(this, hittableMask, direction);
            throwable.GetComponent<Throwable>().EnableBullet();
        }    
    }
}