using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    [CreateAssetMenu(fileName = "Bullet Data", menuName = "Bullet/BulletData")]
    public class BulletData : ScriptableObject
    {
        public string Name;
        public int ID = 0;

        public GameObject prefab;

        public float gravityScale = 1;
        public float range = 1;
        public float xSpeed = 1;
        public int damage = 1;

        public AudioClip shootSound;

        public void PerformShoot(Agent agent, Vector3 shootDirection, LayerMask hittableMask)
        {
            Debug.Log("Bullet used: " + Name);

            //agent.agentBulletManager.ToggleWeaponVisibility(false);
            GameObject bullet = Instantiate(prefab, agent.agentBulletManager.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Initialize(this, shootDirection, hittableMask);
        }
    }
}