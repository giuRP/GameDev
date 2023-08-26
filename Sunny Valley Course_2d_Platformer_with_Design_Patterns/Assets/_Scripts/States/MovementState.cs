using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementState : State
{
    [SerializeField]
    protected MovementStateData movementData;

    public UnityEvent OnStep;

    private void Awake()
    {
        movementData = GetComponentInParent<MovementStateData>();
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run);
        agent.animationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());

        movementData.horizontalMovementDirection = 0;
        movementData.currentSpeed = 0;
        movementData.currentVelocity = Vector2.zero;
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }

    public override void StateUpdate()
    {
        if (TestFallTransition())
            return;
        
        CalculateVelocity();
        SetPlayerVelocity();

        if (Mathf.Abs(agent.rb2d.velocity.x) < 0.01f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementDirection, movementData);
        CalculateHorizontalDirection(movementData);

        movementData.currentVelocity = Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;
        movementData.currentVelocity.y = agent.rb2d.velocity.y;
    }

    protected void CalculateSpeed(Vector2 movementDirection, MovementStateData movementData)
    {
        if (Mathf.Abs(movementDirection.x) > 0)
        {
            movementData.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
        }
        else
        {
            movementData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;
        }

        movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, agent.agentData.maxSpeed);
    }

    protected void CalculateHorizontalDirection(MovementStateData movementData)
    {
        if (agent.agentInput.MovementDirection.x > 0)
        {
            movementData.horizontalMovementDirection = 1;
        }
        else if(agent.agentInput.MovementDirection.x < 0)
        {
            movementData.horizontalMovementDirection = -1;
        }
    }

    protected void SetPlayerVelocity()
    {
        agent.rb2d.velocity = movementData.currentVelocity;
    }
}
