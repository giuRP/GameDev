using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIBehaviourMeleeAttack : AIBehaviour
    {
        public AIMeleeAttackDetector meleeAttackDetector;

        [SerializeField]
        private bool isWaiting;

        [SerializeField]
        private float attackDelay = 1;

        private void Awake()
        {
            if (meleeAttackDetector == null)
                meleeAttackDetector = transform.parent.GetComponentInParent<AIMeleeAttackDetector>();
        }

        public override void PerformAction(AIEnemy enemyAI)
        {
            if (isWaiting)
                return;

            if (meleeAttackDetector.PlayerDetected == false)
                return;

            enemyAI.CallOnAttack();
            isWaiting = true;
            StartCoroutine(AttackDelayCoroutine());
        }

        IEnumerator AttackDelayCoroutine()
        {
            yield return new WaitForSeconds(attackDelay);
            isWaiting = false;
        }
    }
}