using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : State
{
    public LayerMask hittableLayerMask;

    protected Vector2 direction;

    public UnityEvent<AudioClip> OnWeaponSound;

    [SerializeField]
    private bool showGizmos = false;

    protected override void EnterState()
    {
        agent.animationManager.ResetEvents();
        agent.animationManager.PlayAnimation(AnimationType.attack);
        agent.animationManager.OnAnimationAction.AddListener(PerformAttack);
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToChoseState);

        agent.agentInput.OnMovement -= agent.agentRenderer.FaceDirection;

        agent.agentWeapon.ToggleWeaponVisibility(true);

        direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);

        if (agent.groundDetector.isGrounded)
            agent.rb2d.velocity = Vector2.zero;
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(agent.agentWeapon.GetCurrentWeapon().weaponSwingSound);
        agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);
        agent.agentWeapon.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, direction);
    }

    private void TransitionToChoseState() //Nesse caso é para Idle ou Fall states;
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToChoseState);

        if (agent.groundDetector.isGrounded)
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        else
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
    }

    public override void StateUpdate()
    {
        //Prevent Update;
    }

    public override void StateFixedUpdate()
    {
        //Prevent Fixed Update;
    }

    protected override void HandleAttack()
    {
        //Prevent Attacking again; When we are in the attack state,
        //the only way to transition back is when we finish the attack animation;
    }

    protected override void HandleJumpPressed()
    {
        //Don't allow jumping;
    }

    protected override void HandleJumpReleased()
    {
        //Just for safety;
    }

    protected override void HandleMovement(Vector2 obj)
    {
        //Stop flipping / rotation our agent;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        if (showGizmos == false)
            return;
        if (agent.agentWeapon.GetCurrentWeapon() == null)
            return;

        Gizmos.color = Color.red;
        var pos = agent.agentWeapon.transform.position;
        agent.agentWeapon.GetCurrentWeapon().DrawWeaponGizmo(pos, direction);
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
        agent.agentInput.OnMovement += agent.agentRenderer.FaceDirection;

        agent.agentWeapon.ToggleWeaponVisibility(false);
    }
}
