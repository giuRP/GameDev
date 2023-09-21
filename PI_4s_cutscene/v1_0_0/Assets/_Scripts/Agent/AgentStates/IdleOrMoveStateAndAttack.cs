using PI4.BulletSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class IdleOrMoveStateAndAttack : State
{
    [SerializeField]
    protected MovementData movementData;

    public UnityEvent OnStep;
    public UnityEvent<AudioClip> OnShootSound;

    protected Vector2 shootDirection;
    public LayerMask hittableLayerMask;

    private void Awake()
    {
        movementData = GetComponentInParent<MovementData>();
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.idleOrMovementAndAttack);
        agent.animationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());

        movementData.currentVelocity = agent.rb2d.velocity;

        shootDirection = agent.transform.right;
    }

    public override void StateUpdate()
    {
        CalculateVelocity();
        SetPlayerVelocity();
    }

    protected void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementDirection, movementData);

        float xDirection = agent.agentInput.MovementDirection.x;
        float yDirection = agent.agentInput.MovementDirection.y;

        movementData.currentVelocity = new Vector2(xDirection * movementData.currentSpeed, yDirection * movementData.currentSpeed);
    }

    protected void CalculateSpeed(Vector2 movementDirection, MovementData movementData)
    {
        if (Mathf.Abs(movementDirection.x) > 0 || Mathf.Abs(movementDirection.y) > 0)
        {
            movementData.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
        }
        else
        {
            movementData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;
        }

        movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, agent.agentData.maxSpeed);
    }

    protected void SetPlayerVelocity()
    {
        agent.rb2d.velocity = movementData.currentVelocity;
    }

    protected override void HandleAttack()
    {
        //OnShootSound?.Invoke(agent.agentBulletManager.GetCurrentBullet().bulletShootSound);
        agent.agentBulletManager.GetCurrentBullet().PerformShoot(agent, shootDirection, hittableLayerMask);
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }
}