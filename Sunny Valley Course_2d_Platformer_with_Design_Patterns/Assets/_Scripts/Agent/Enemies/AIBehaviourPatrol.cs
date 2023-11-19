using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIBehaviourPatrol : AIBehaviour
    {
        public AIEndPlatformDetector endPlatformDetector;

        private Vector2 movementDirection = Vector2.zero;

        private void Awake()
        {
            if (endPlatformDetector == null)
                endPlatformDetector = transform.parent.GetComponentInParent<AIEndPlatformDetector>();
        }

        private void Start()
        {
            endPlatformDetector.OnPathBlocked += HandlePathBlocked;
            movementDirection = new Vector2(Random.value > 0.5f ? 1 : -1, 0);
        }

        private void HandlePathBlocked()
        {
            movementDirection *= new Vector2(-1, 0);
        }

        public override void PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementDirection = movementDirection;
            enemyAI.CallOnMovement(movementDirection);
        }
    }
}