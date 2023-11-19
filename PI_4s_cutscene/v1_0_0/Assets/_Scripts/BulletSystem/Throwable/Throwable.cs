using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.BulletSystem
{
    public abstract class Throwable : MonoBehaviour
    {
        public _BulletData bulletData;
        public bool isInitialize = false;

        public UnityEvent OnEnableBullet;
        public UnityEvent OnDisableBullet;

        public abstract void Initialize(_BulletData data, LayerMask hittableLayerMask, Vector2 shootDirection);

        public abstract void EnableBullet();

        public abstract void UpdateBullet();

        public abstract void DisableBullet();
    }
}