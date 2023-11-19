using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public float timeToWaitBeforeDieAction = 2;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.die);
        agent.animationManager.OnAnimationEnd.AddListener(WaitBeforeDieAction);
        agent.rb2d.gravityScale = 1;
        //agent.agentInput.OnMovement -= agent.agentRenderer.FaceDirection;
    }

    private void WaitBeforeDieAction()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(WaitBeforeDieAction);
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(timeToWaitBeforeDieAction);
        agent.OnAgentDie?.Invoke();
    }

    public override void StateUpdate()
    {
        agent.rb2d.velocity = new Vector2(0, agent.rb2d.velocity.y);
    }

    //Prevent transition to other states:

    protected override void HandleAttack()
    {
        //prevent attack
    }

    public override void HandleGetHit()
    {
        //prevent get hit twice
        //Ainda nao previne de perder vida - imortalidade temporária - 
    }

    public override void HandleDie()
    {
        //prevent die again
    }

    protected override void ExitState()
    {
        StopAllCoroutines();
        agent.animationManager.ResetEvents();
    }
}
