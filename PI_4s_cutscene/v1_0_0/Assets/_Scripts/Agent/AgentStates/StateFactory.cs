using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State IdleOrMovementAndAttack, GetHit, Die;

    public State GetState(StateType stateType) => stateType switch
    {
        StateType.IdleOrMovementAndAttack => IdleOrMovementAndAttack,
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
    IdleOrMovementAndAttack,
    GetHit,
    Die
}
