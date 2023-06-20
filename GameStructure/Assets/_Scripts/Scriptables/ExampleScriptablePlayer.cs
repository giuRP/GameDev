using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ExampleScriptablePlayer : ExampleScriptableUnitBase
{
    public ExamplePlayerClass playerClass;
}

[Serializable]
public enum ExamplePlayerClass
{
    Rogue = 0,
    Sorcerer = 1,
    Barbarian = 2
}
