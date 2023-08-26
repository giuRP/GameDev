using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Range Weapon Data", menuName = "Weapons/RangeWeaponData")]
    public class RangeWeaponData : WeaponData
    {
        public GameObject rangeWeaponPrefab;

        public float weaponThrowSpeed = 1;

        public override bool CanBeUsed(bool isGrounded)
        {
            return true;
        }

        public override void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction)
        {
            Debug.Log("Weapon used: " + weaponName);

            agent.agentWeapon.ToggleWeaponVisibility(false);
            GameObject throwable = Instantiate(rangeWeaponPrefab, agent.agentWeapon.transform.position, Quaternion.identity);
            throwable.GetComponent<ThrowableWeapon>().Initialize(this, direction, hittableMask);
        }
    }
}