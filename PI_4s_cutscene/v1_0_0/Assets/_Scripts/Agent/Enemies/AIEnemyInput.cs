using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public abstract class AIEnemyInput : MonoBehaviour, IAgentInput
    {
        public Vector2 MovementDirection { get; set; }

        public event Action OnAttack;
        public event Action<Vector2> OnMovement;

        public void CallOnAttack()
        {
            OnAttack?.Invoke();
        }

        public void CallOnMovement(Vector2 input)
        {
            OnMovement?.Invoke(input);
        }
    }
}