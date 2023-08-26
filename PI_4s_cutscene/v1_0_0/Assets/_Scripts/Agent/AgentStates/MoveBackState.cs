using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveBackState : IdleOrMoveState
{
    public UnityEvent OnBreak;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.moveBack);
        agent.animationManager.OnAnimationAction.AddListener(() => OnBreak.Invoke());

        //movementData.horizontalMovementDirection = -1;
        movementData.currentVelocity = agent.rb2d.velocity;
    }

    public override void StateUpdate()
    {
        CalculateVelocity();
        SetPlayerVelocity();
    }

    protected override void HandleMovement(Vector2 input)
    {
        if (input.x >= 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.IdleOrMovement));
        }
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }
}
