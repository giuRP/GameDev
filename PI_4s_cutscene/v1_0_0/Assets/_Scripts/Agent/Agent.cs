using PI4.BulletSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Agent : MonoBehaviour
{
    public AgentDataSO agentData;

    public Rigidbody2D rb2d;
    public IAgentInput agentInput;
    public AgentAnimation animationManager;
    public AgentRenderer agentRenderer;

    [HideInInspector]
    public AgentBulletManager agentBulletManager;

    //private Damageable damageable;

    public StateFactory stateFactory;

    public State currentState = null, previousState = null;

    [Header("State Debbuging: ")]
    public string stateName = "";

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }

    [field: SerializeField]
    public UnityEvent OnAgentDie { get; set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        agentInput = GetComponentInParent<IAgentInput>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();

        //groundDetector = GetComponentInChildren<GroundDetector>();
        //climbingDetector = GetComponentInChildren<ClimbingDetector>();
        agentBulletManager = GetComponentInChildren<AgentBulletManager>();
        stateFactory = GetComponentInChildren<StateFactory>();
        //damageable = GetComponent<Damageable>();

        stateFactory.InitializeStates(this);
    }

    private void Start()
    {
        InitializeAgent();

        //agentInput.OnWeaponChange += SwapWeapon;
    }

    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.IdleOrMovementAndAttack));
        //damageable.InitializeHealth(agentData.health);
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

    public void AgentDied() //callback em um evento do damageable
    {
        //if (damageable.CurrentHealth > 0)
        //{
        //    OnRespawnRequired?.Invoke();
        //}
        //else
        //{
        //    currentState.HandleDie();
        //}
    }
}
