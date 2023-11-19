using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.BulletSystem
{
    public abstract class _BulletData : ScriptableObject, IComparable<_BulletData>
    {
        public string bulletName;

        public int damage = 1;
        public float range = 2;
        public float throwSpeed = 0;
        public float durationTime = 0;
        public float attackCoolDown = 0;

        public GameObject prefab;
        public Sprite sprite;
        public AudioClip shootSound;

        public int CompareTo(_BulletData other)
        {
            if (bulletName == other.bulletName)
                return 0;
            else
                return -1;
        }

        public abstract void StartBullet(AgentWeaponManager weapon);

        public abstract void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction);

        public virtual void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {

        }
    }
}