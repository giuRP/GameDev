using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementDirection { get; }

    event Action OnAttack;
    event Action<Vector2> OnMovement;
}
