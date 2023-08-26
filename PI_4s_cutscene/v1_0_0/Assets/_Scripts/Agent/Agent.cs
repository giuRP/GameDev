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

    //[HideInInspector]
    //public AgentWeaponManager agentWeapon;

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
        //agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        stateFactory = GetComponentInChildren<StateFactory>();
        //damageable = GetComponent<Damageable>();

        stateFactory.InitializeStates(this);
    }

    private void Start()
    {
        //agentInput.OnMovement += agentRenderer.FaceDirection;
        InitializeAgent();

        //agentInput.OnWeaponChange += SwapWeapon;
    }

    private void InitializeAgent()
    {
        TransitionToState(stateFactory.GetState(StateType.IdleOrMovement));
        //damageable.InitializeHealth(agentData.health);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        //groundDetector.CheckIsGrounded();
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
}
