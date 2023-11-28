using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIBossMoveBehaviour : AIBehaviour
    {
        [SerializeField]
        private Agent agent;

        Vector2 movementDirection = Vector2.zero;

        private void Awake()
        {
            movementDirection = -transform.right;
        }

        private void Update()
        {
            if (agent.transform.position.x < 7.5f)
            {
                movementDirection = Vector2.zero;
                agent.data.maxSpeed = 0;
            }
        }

        public override void PerformAction(AIEnemyInput enemyAI)
        {
            enemyAI.MovementDirection = movementDirection;
            enemyAI.CallOnMovement(enemyAI.MovementDirection);
        }
    }
}