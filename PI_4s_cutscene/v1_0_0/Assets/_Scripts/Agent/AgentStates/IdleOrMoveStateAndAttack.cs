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

    public UnityEvent OnGetHit;

    private bool canAttack = true;

    public UnityEvent<AudioClip> OnShootSound;
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
            movementData.currentSpeed += agent.data.acceleration * Time.deltaTime;
        }
        else
        {
            movementData.currentSpeed -= agent.data.deacceleration * Time.deltaTime;
        }

        movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, agent.data.maxSpeed);
    }

    protected void SetPlayerVelocity()
    {
        agent.rb2d.velocity = movementData.currentVelocity;
    }

    protected override void HandleAttack()
    {
        if (!canAttack)
            return;

        canAttack = false;
        agent.weapon.GetCurrentBullet().PerformAttack(agent, this.hittableLayerMask, agent.data.shootDirection);
        OnShootSound?.Invoke(agent.weapon.GetCurrentBullet().shootSound);

        StartCoroutine(AttackDalay());
    }

    private IEnumerator AttackDalay()
    {
        yield return new WaitForSeconds(agent.weapon.GetCurrentBullet().attackCoolDown);
        canAttack = true;
    }

    public override void HandleGetHit()
    {
        //Imortalidade temporária
        //Solid color shader
        OnGetHit?.Invoke();
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        
        if (agent.weapon.GetCurrentBullet() == null)
            return;

        Gizmos.color = Color.red;
        var pos = agent.weapon.transform.position;
        agent.weapon.GetCurrentBullet().DrawWeaponGizmo(pos, agent.data.shootDirection);
    }
}