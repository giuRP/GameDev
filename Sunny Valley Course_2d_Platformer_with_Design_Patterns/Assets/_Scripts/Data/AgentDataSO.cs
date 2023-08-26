using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentData", menuName = "Agent/Data")]
public class AgentDataSO : ScriptableObject
{
    [Header("Health Data")]
    [Space]
    public int health = 2;


    [Header("Movement Data")]
    [Space]
    public float maxSpeed = 6;
    public float acceleration = 50;
    public float deacceleration = 50;

    [Header("Jump Data")]
    [Space]
    public float jumpForce = 12;
    public float lowJumpMultplier = 2;
    public float gravityModifier = 0.5f;

    [Header("Climb Data")]
    [Space]
    public float climbHorizontalSpeed = 2;
    public float climbVerticalSpeed = 5;
}
