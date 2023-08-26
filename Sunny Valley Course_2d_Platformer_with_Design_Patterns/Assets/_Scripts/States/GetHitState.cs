using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : State
{
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.hit);
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToChoseState);
    }

    private void TransitionToChoseState()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToChoseState);
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
    }

    //Prevent transition to other states:

    public override void StateUpdate()
    {
        //prevent update
    }

    protected override void HandleAttack()
    {
        //prevent attack
    }

    protected override void HandleJumpPressed()
    {
        //prevent jump
    }

    public override void HandleGetHit()
    {
        //prevent get hit twice
        //Ainda nao previne de perder vida - imortalidade temporária - 
    }
}
