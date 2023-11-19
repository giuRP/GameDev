using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIPatrollingEnemyBrain : AIEnemyInput
    {
        public AIBehaviour attackBehaviour, patrolBehaviour;

        private void Update()
        {
            patrolBehaviour.PerformAction(this);
        }
    }
}