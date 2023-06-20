using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScriptableUnitBase : ScriptableObject
{
    public UnitType type;

    [SerializeField]
    private Stats _stats;
    public Stats baseStats => _stats;

    //Used in game
    public GameObject Prefab;

    //Used in menus
    public string Description;
    public Sprite MenuSprite;
}

[Serializable]
public struct Stats
{
    public int Health;
    public int Damage;
    public float MoveSpeed;
}

[Serializable]
public enum UnitType
{
    player = 0,
    normalEnemy = 1,
    bossEnemy = 2
}
