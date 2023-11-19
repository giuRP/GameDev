using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIAttackBehaviour : AIBehaviour
    {
        [SerializeField]
        private bool isWaiting;

        [SerializeField]
        private float attackDelay = 1;

        public override void PerformAction(AIEnemyInput enemyAI)
        {
            if (isWaiting)
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