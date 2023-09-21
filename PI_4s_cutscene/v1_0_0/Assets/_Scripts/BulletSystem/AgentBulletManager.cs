using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.BulletSystem
{
    public class AgentBulletManager : MonoBehaviour
    {
        private Sprite bulletSprite;
        
        public UnityEvent<Sprite> OnWeaponSwap;
        public UnityEvent OnWeaponPickUp;

        private int currentBulletID;

        [Header("Gizmos Parameters")]
        Color gizmosColor = Color.red;

        private void Start()
        {
            currentBulletID = 0;
            GetCurrentBullet();
        }

        public BulletData GetCurrentBullet()
        {
            return BulletStorage.Instance.bulletDataList[currentBulletID];
        }

        public void PickUpSpecialBullet(BulletData bulletData)
        {
            currentBulletID = bulletData.ID;

            GetCurrentBullet();

            OnWeaponPickUp?.Invoke();
        }

        //private void OnDrawGizmos()
        //{
        //    float range = GetCurrentBullet().range;

        //    Gizmos.color = gizmosColor;
        //    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + range, transform.position.y));
        //}

        private void OnDrawGizmosSelected()
        {
            float range = GetCurrentBullet().range;

            Gizmos.color = gizmosColor;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + range, transform.position.y));
        }
    }
}