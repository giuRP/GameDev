using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public abstract class AIBehaviour : MonoBehaviour
    {
        public abstract void PerformAction(AIEnemyInput enemyAI);
    }
}