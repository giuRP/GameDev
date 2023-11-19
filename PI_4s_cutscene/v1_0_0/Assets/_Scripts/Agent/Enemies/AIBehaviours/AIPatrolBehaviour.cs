using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.AI
{
    public class AIPatrolBehaviour : AIBehaviour
    {
        private Vector2 movementDirection = Vector2.zero;

        [SerializeField]
        private EndPositionDetector positionDetector;

        [SerializeField]
        private SpriteRenderer avatarSR;

        public UnityEvent OnEndPosition;

        private void Start()
        {
            positionDetector.OnArrivedAtEndPosition += HandleArrivedAtEndPosition;
            positionDetector.OnArrivedAtStartPosition += HandleArrivedAtStartPosition;

            movementDirection = -(transform.right);

            avatarSR.flipX = false;
        }

        private void HandleArrivedAtEndPosition()
        {
            StopAllCoroutines();

            StartCoroutine(WaitToMove(2));
            OnEndPosition?.Invoke();
        }

        private void HandleArrivedAtStartPosition()
        {
            StopAllCoroutines();

            StartCoroutine(WaitToMove(7));
        }

        public override void PerformAction(AIEnemyInput enemyAI)
        {
            //Debug.Log("andou");
            enemyAI.MovementDirection = movementDirection;
            enemyAI.CallOnMovement(enemyAI.MovementDirection);
        }

        IEnumerator WaitToMove(float timeToWait)
        {
            Vector2 vec2 = movementDirection * new Vector2(-1, 0);
            movementDirection = Vector2.zero;

            yield return new WaitForSeconds(timeToWait);
            movementDirection = vec2;
            avatarSR.flipX = !avatarSR.flipX;
        }
    }
}