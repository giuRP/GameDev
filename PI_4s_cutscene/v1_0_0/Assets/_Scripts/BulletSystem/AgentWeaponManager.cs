using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.BulletSystem
{
    public class AgentWeaponManager : MonoBehaviour
    {
        [SerializeField]
        private List<_BulletData> bulletDataList = new List<_BulletData>();

        [SerializeField]
        private _BulletData currentBullet;

        private _BulletData defaultBullet;

        public UnityEvent OnPickUpBullet;
        public UnityEvent<Sprite> OnSwapBulletSpriteUI;

        public _BulletData GetCurrentBullet()
        {
            return currentBullet;
        }

        public void SetUpBullet(_BulletData bullet)
        {
            if (bullet == null)
                return;

            if(defaultBullet == null)
            {
                defaultBullet = bullet;
            }

            AddBulletData(bullet);

            SwapBulletSpriteUI(currentBullet.sprite);
        }

        public void PickUpBullet(_BulletData bulletPickable)
        {
            StopAllCoroutines();

            OnPickUpBullet?.Invoke();

            SetUpBullet(bulletPickable);

            if (bulletPickable.durationTime != 0)
            {
                StartCoroutine(ReturnToDefaultBullet(bulletPickable.durationTime));
            }
        }
        
        private void AddBulletData(_BulletData newBullet)
        {
            int index = bulletDataList.BinarySearch(newBullet);

            if (index < 0)
            {
                //Debug.Log("Not exist" + index);
                bulletDataList.Add(newBullet);
                newBullet.StartBullet(this);
                currentBullet = newBullet;
            }
            else
            {
                //Debug.Log("Exist" + index);
                currentBullet = bulletDataList[index];
            }
        }

        private void SwapBulletSpriteUI(Sprite bulletSpriteUI)
        {
            OnSwapBulletSpriteUI?.Invoke(bulletSpriteUI);
        }    

        private IEnumerator ReturnToDefaultBullet(float bulletDurationTime)
        {
            //Debug.Log("CoolDown");
            yield return new WaitForSeconds(bulletDurationTime);
            SetUpBullet(defaultBullet);
        }
    }
}