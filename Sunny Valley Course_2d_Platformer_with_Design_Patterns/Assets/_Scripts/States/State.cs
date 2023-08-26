using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;

    public UnityEvent OnEnter, OnExit;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    public void Enter()
    {
        this.agent.agentInput.OnAttack += HandleAttack;
        this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
        this.agent.agentInput.OnMovement += HandleMovement;

        OnEnter?.Invoke();

        EnterState();
    }

    public void Exit()
    {
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
        this.agent.agentInput.OnMovement -= HandleMovement;

        OnExit?.Invoke();

        ExitState();
    }

    protected virtual void EnterState()
    {
    }

    protected virtual void ExitState()
    {
    }

    public virtual void StateUpdate()
    {
        TestFallTransition();
    }

    public virtual void StateFixedUpdate()
    {
    }

    protected virtual void HandleMovement(Vector2 obj)
    {
    }

    protected virtual void HandleJumpPressed()
    {
        TestJumpTransition();
    }

    protected virtual void HandleJumpReleased()
    {
    }

    protected virtual void HandleAttack()
    {
        TestAttackTransition();
    }

    public virtual void HandleGetHit()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.GetHit));
    }

    public virtual void HandleDie()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Die));
    } 

    private void TestJumpTransition()
    {
        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
        }
    }

    protected bool TestFallTransition()
    {
        if (agent.groundDetector.isGrounded == false)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
            return true;
        }
        return false;
    }

    protected virtual void TestAttackTransition()
    {
        if (agent.agentWeapon.CanIUseWeapon(agent.groundDetector.isGrounded))
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
        }
    }
}
