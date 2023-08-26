using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour
{
    public AgentDataSO agentData;

    public Rigidbody2D rb2d;
    public IAgentInput agentInput;
    public AgentAnimation animationManager;
    public AgentRenderer agentRenderer;
    public GroundDetector groundDetector;
    public ClimbingDetector climbingDetector;

    public State currentState = null, previousState = null;

    [HideInInspector]
    public AgentWeaponManager agentWeapon;

    public StateFactory stateFactory;

    [Header("State Debbuging: ")]
    public string stateName = "";

    private Damageable damageable;

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }

    [field: SerializeField]
    public UnityEvent OnAgentDie { get; set; }

    private void Awake()
    {
        agentInput = GetComponentInParent<IAgentInput>();
        rb2d = GetComponent<Rigidbody2D>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        groundDetector = GetComponentInChildren<GroundDetector>();
        climbingDetector = GetComponentInChildren<ClimbingDetector>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        stateFactory = GetComponentInChildren<StateFactory>();
        damageable = GetComponent<Damageable>();

        stateFactory.InitializeStates(this);     
    }

    private void Start()
    {
        agentInput.OnMovement += agentRenderer.FaceDirection;
        InitializeAgent();

        agentInput.OnWeaponChange += SwapWeapon;
    }

    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.Idle));
        damageable.InitializeHealth(agentData.health);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        groundDetector.CheckIsGrounded();
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
        if(previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

    public void PickUpWeapon(WeaponData weaponData)
    {
        if (agentWeapon == null)
            return;

        agentWeapon.PickUpWeapon(weaponData);
    }

    private void SwapWeapon()
    {
        if (agentWeapon == null)
            return;

        agentWeapon.SwapWeapon();
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
