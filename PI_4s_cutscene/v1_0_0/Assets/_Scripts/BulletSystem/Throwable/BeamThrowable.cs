using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.BulletSystem
{
    public class BeamThrowable : Throwable
    {
        Vector2 direction;

        LayerMask layerMask;

        LineRenderer lr;

        public UnityEvent OnUpdateBullet;

        public override void Initialize(_BulletData data, LayerMask hittableLayerMask, Vector2 shootDirection)
        {
            isInitialize= true;

            bulletData = data;
            layerMask = hittableLayerMask;
            direction = shootDirection;

            lr = GetComponent<LineRenderer>();
        }

        public override void EnableBullet()
        {
            OnEnableBullet?.Invoke();          
        }

        private void Update()
        {
            if (isInitialize == false)
                return;

            lr.SetPosition(0, gameObject.GetComponentInParent<Transform>().position);
            lr.SetPosition(1, new Vector3(gameObject.GetComponentInParent<Transform>().position.x + direction.x * bulletData.range, 0, 0));
        }

        public override void UpdateBullet()
        {
            
        }

        public override void DisableBullet()
        {
            OnDisableBullet?.Invoke();  
        }
    }
}