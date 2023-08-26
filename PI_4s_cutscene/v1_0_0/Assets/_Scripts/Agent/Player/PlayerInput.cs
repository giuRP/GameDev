using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour, IAgentInput
{
    [field : SerializeField]
    public Vector2 MovementDirection { get; private set; }

    public event Action OnAttack, OnMoveBackPressed, OnMoveBackReleased;

    public event Action<Vector2> OnMovement;

    public KeyCode attackKey, menuKey;

    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            GetMovementInput();
            GetMoveBackInput();
            GetAttackInput();
        }

        GetMenuInput();
    }

    private void GetMenuInput()
    {
        if (Input.GetKeyDown(menuKey))
        {
            OnMenuKeyPressed?.Invoke();
        }
    }

    private void GetAttackInput()
    {
        if (Input.GetKeyDown(attackKey))
        {
            OnAttack?.Invoke();
        }
    }

    private void GetMoveBackInput()
    {
        if (Input.GetKeyDown(KeyCode.A)) //GetMovementDirection().x < 0 
        {
            OnMoveBackPressed?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.A)) //GetMovementDirection().x > 0
        {
            OnMoveBackReleased?.Invoke();
        }
    }

    private void GetMovementInput()
    {
        MovementDirection = GetMovementDirection();
        OnMovement?.Invoke(MovementDirection);
    }

    protected Vector2 GetMovementDirection()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
