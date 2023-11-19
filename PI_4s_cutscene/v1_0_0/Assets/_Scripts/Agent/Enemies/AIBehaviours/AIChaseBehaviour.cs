using PI4.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIChaseBehaviour : AIBehaviour
{
    public override void PerformAction(AIEnemyInput enemyAI)
    {
        enemyAI.CallOnMovement(enemyAI.MovementDirection);
    }
}
