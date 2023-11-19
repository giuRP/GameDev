using PI4.BulletSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.PickableSystem
{
    public class BulletPickable : Pickable
    {
        [SerializeField]
        private _BulletData bulletData;

        private void Start()
        {
            spriteRenderer.sprite = bulletData.sprite;
        }

        public override void PickUp(Agent agent)
        {
            agent.PickUpBullet(bulletData);
        }
    }
}