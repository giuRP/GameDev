using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIStaticEnemyBrain : AIEnemyInput
    {
        public AIBehaviour attackBehaviour;

        [SerializeField]
        private Agent agent;

        [SerializeField]
        private PlayerDetector playerDetector;

        private Vector2 startPosition = Vector2.zero;
        private Vector2 movementVector = Vector2.zero;

        bool canMove = true;
        float moveDelay = 5f;

        private void Awake()
        {
            agent = GetComponentInChildren<Agent>();
            playerDetector = GetComponentInChildren<PlayerDetector>();
        }

        private void Start()
        {
            startPosition = agent.transform.position;

            playerDetector.OnDetectedTarget += PerformAttack;
        }

        private void Update()
        {
            if (canMove)
            {
                MovementDirection = -(transform.right);
            }
            else
            {
                MovementDirection = Vector2.zero;
            }
                
            CallOnMovement(MovementDirection);
        }

        private void PerformAttack()
        {
            attackBehaviour.PerformAction(this);
        }

        public void ReturnToStartPosition()
        {
            agent.transform.position = startPosition;
            canMove = false;
            StartCoroutine(MoveDelay());
        }

        IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(moveDelay);
            canMove = true;
        }
    }
}