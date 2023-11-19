using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemyToRespawn;

    GameObject enemySpawned;

    [SerializeField]
    float spawnDelay = 1f;

    private void Awake()
    {
        StartCoroutine(RespawnCoroutine(spawnDelay));
    }

    private void Update()
    {
        if (enemySpawned.IsDestroyed())
        {
            RespawnRequest();
        }
    }

    private void RespawnRequest()
    {
        enemySpawned = Instantiate(enemyToRespawn, transform.position, Quaternion.identity);
    }

    IEnumerator RespawnCoroutine(float timeToRespawn)
    {
        yield return new WaitForSeconds(timeToRespawn);
        RespawnRequest();
    }
}
