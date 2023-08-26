using System;
using UnityEngine;

public interface IAgentInput
{
    Vector2 MovementDirection { get; }

    event Action OnAttack;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action<Vector2> OnMovement;
    event Action OnWeaponChange;
}