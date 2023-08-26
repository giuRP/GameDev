using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallState : MovementState
{
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.fall);
    }

    public override void StateUpdate()
    {
        ControlFallVelocity();
        CalculateVelocity();
        SetPlayerVelocity();

        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
        else if(agent.climbingDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementDirection.y) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climb));
        }
    }

    protected override void HandleJumpPressed()
    {
        //Dont allow jumping in fall state
    }

    private void ControlFallVelocity()
    {
        movementData.currentVelocity = agent.rb2d.velocity;
        movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;
        agent.rb2d.velocity = movementData.currentVelocity;
    }
}
