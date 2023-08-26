using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State Idle, Movement, Jump, Fall, Climb, Attack, GetHit, Die;

    public State GetState(StateType stateType) => stateType switch
    {
        StateType.Idle => Idle,
        StateType.Movement => Movement,
        StateType.Jump => Jump,
        StateType.Fall => Fall,
        StateType.Climb => Climb,
        StateType.Attack => Attack,
        StateType.GetHit => GetHit,
        StateType.Die => Die,
        _ => throw new System.Exception("State not defined " + stateType.ToString())
    };

    public void InitializeStates(Agent agent)
    {
        State[] states = GetComponents<State>();
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}

public enum StateType
{
    Idle,
    Movement,
    Jump,
    Fall,
    Climb,
    Attack,
    GetHit,
    Die
}