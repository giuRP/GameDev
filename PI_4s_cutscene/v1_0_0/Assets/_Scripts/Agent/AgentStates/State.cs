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
        this.agent.agentInput.OnMoveBackPressed += HandleMoveBackPressed;
        this.agent.agentInput.OnMoveBackReleased += HandleMoveBackReleased;
        this.agent.agentInput.OnMovement += HandleMovement;

        OnEnter?.Invoke();

        EnterState();
    }

    protected virtual void EnterState()
    {
    }

    public virtual void StateUpdate()
    {
        //TestFallTransition();
    }

    public virtual void StateFixedUpdate()
    {
    }

    protected virtual void HandleMovement(Vector2 input)
    {
    }

    protected virtual void HandleMoveBackPressed()
    {
        
    }

    protected virtual void HandleMoveBackReleased()
    {
    }

    protected virtual void HandleAttack()
    {
        //TestAttackTransition();
    }

    public virtual void HandleGetHit()
    {
        //agent.TransitionToState(agent.stateFactory.GetState(StateType.GetHit));
    }

    public virtual void HandleDie()
    {
        //agent.TransitionToState(agent.stateFactory.GetState(StateType.Die));
    }

    public void Exit()
    {
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnMoveBackPressed += HandleMoveBackPressed;
        this.agent.agentInput.OnMoveBackReleased += HandleMoveBackReleased;
        this.agent.agentInput.OnMovement -= HandleMovement;

        OnExit?.Invoke();

        ExitState();
    }

    protected virtual void ExitState()
    {
    }
}
