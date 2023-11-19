using PI4.BulletSystem;
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

    [Header("Bullet Data")]
    [Space]
    public _BulletData defaultBullet;
    public Vector2 shootDirection;
}
