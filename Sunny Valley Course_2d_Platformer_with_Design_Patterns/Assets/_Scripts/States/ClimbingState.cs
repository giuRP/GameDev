using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : State
{
    public float previousGravityScale = 0;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.climb);
        agent.animationManager.StopAnimation();

        previousGravityScale = agent.rb2d.gravityScale;
        agent.rb2d.gravityScale = 0;
        agent.rb2d.velocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        if (agent.agentInput.MovementDirection.magnitude > 0)
        {
            agent.animationManager.StartAnimation();
            agent.rb2d.velocity = new Vector2(agent.agentInput.MovementDirection.x * agent.agentData.climbHorizontalSpeed,
                agent.agentInput.MovementDirection.y * agent.agentData.climbVerticalSpeed);
        }
        else
        {
            agent.animationManager.StopAnimation();
            agent.rb2d.velocity = Vector2.zero;
        }

        if (agent.climbingDetector.CanClimb == false)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected override void HandleJumpPressed()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
    }

    protected override void TestAttackTransition()
    {
        //Prevent Attack
    }

    protected override void ExitState()
    {
        agent.animationManager.StartAnimation();

        agent.rb2d.gravityScale = previousGravityScale;
    }
}
