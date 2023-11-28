using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PI4.RespawnSystem
{
    public class RespawnHelper : MonoBehaviour
    {
        public GameObject enemyToRespawn;
        public GameObject enemySpawned;

        [SerializeField]
        private float respawnDelay = 0f;

        bool canSpawn = true;

        public void RespawnRequest()
        {
            if (!canSpawn)
                return;
                       
            StartCoroutine(Respawn());
        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(respawnDelay);

            enemySpawned = Instantiate(enemyToRespawn, transform.position, Quaternion.identity);
            //canSpawn = false;

            //yield return new WaitForSeconds(10);
            //canSpawn = true;
        }
    }
}