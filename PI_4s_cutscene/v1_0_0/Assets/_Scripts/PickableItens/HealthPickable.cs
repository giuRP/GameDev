using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.PickableSystem
{
    public class HealthPickable : Pickable
    {
        [SerializeField]
        private int healAmount = 1;

        public override void PickUp(Agent agent)
        {
            Damageable damageable = agent.GetComponent<Damageable>();

            if (damageable == null)
                return;

            damageable.Heal(healAmount);
        }
    }
}