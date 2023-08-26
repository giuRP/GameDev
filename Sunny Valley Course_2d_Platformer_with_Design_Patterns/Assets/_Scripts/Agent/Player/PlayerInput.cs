using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour, IAgentInput
{
    [SerializeField]
    public Vector2 MovementDirection { get; private set; }

    public event Action OnAttack, OnJumpPressed, OnJumpReleased, OnWeaponChange;

    public event Action<Vector2> OnMovement;

    public KeyCode jumpKey, attackKey, menuKey;

    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetWeaponSwapInput();
        }

        GetMenuInput();
    }

    private void GetWeaponSwapInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnWeaponChange?.Invoke();
        }
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

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpPressed?.Invoke();
        }

        if (Input.GetKeyUp(jumpKey))
        {
            OnJumpReleased?.Invoke();
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
