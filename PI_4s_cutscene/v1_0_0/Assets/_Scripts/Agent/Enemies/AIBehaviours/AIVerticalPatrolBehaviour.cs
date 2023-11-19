using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIVerticalPatrolBehaviour : AIBehaviour
    {
        public ScreenEndDetector screenEndDetector;

        private Vector2 movementDirection = Vector2.zero;

        private void Awake()
        {
            if (screenEndDetector == null)
                screenEndDetector = transform.parent.GetComponentInParent<ScreenEndDetector>();
        }

        private void Start()
        {
            screenEndDetector.OnPathBlocked += HandlePathBlocked;
            movementDirection = new Vector2(0, Random.value > 0.5f ? 1 : -1);
            screenEndDetector.rayDirection = movementDirection;
        }

        private void HandlePathBlocked()
        {
            movementDirection *= new Vector2(0, -1);
        }

        public override void PerformAction(AIEnemyInput enemyAI)
        {
            enemyAI.MovementDirection = movementDirection;
            enemyAI.CallOnMovement(movementDirection);
            screenEndDetector.rayDirection = movementDirection;
        }
    }
}