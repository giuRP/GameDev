using PI4.BulletSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Agent : MonoBehaviour
{
    public AgentDataSO data;

    public Rigidbody2D rb2d;
    public IAgentInput agentInput;
    public AgentAnimation animationManager;
    public AgentRenderer agentRenderer;

    [HideInInspector]
    public AgentWeaponManager weapon;

    private Damageable damageable;

    public StateFactory stateFactory;

    public State currentState = null, previousState = null;

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }

    [field: SerializeField]
    public UnityEvent OnAgentDie { get; set; }

    [Header("State Debbuging: ")]
    public string stateName = "";

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        agentInput = GetComponentInParent<IAgentInput>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        weapon = GetComponentInChildren<AgentWeaponManager>();
        stateFactory = GetComponentInChildren<StateFactory>();
        damageable = GetComponent<Damageable>();

        stateFactory.InitializeStates(this);
    }

    private void Start()
    {
        InitializeAgent();
    }

    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.IdleOrMovementAndAttack));

        damageable.InitializeHealth(data.health);

        weapon.SetUpBullet(data.defaultBullet);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

    internal void TransitionToState(State goaltState)
    {
        if (goaltState == null)
            return;

        if (currentState != null)
            currentState.Exit();

        previousState = currentState;
        currentState = goaltState;
        currentState.Enter();

        DisplayState();
    }

    private void DisplayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

    public void PickUpBullet(_BulletData bulletData)
    {
        if (weapon == null)
            return;

        weapon.PickUpBullet(bulletData);
    }

    public void GetHitInCurrentState() //callback em um evento do damageable
    {
        currentState.HandleGetHit();
    }

    public void AgentDied() //callback em um evento do damageable
    {
        if (damageable.CurrentHealth > 0)
        {
            OnRespawnRequired?.Invoke();
        }
        else
        {
            currentState.HandleDie();
        }
    }
}
