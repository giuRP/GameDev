using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleUnitManager : Singleton<ExampleUnitManager>
{
    public void SpawnPlayer()
    {
        Debug.Log("Player foi Spawnado");
    }

    public void SpawnUnit()
    {
        Debug.Log("Objeto foi Spawnado");

    }
}
