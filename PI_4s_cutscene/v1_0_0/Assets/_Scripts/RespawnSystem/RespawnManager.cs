using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.RespawnSystem
{
    public class RespawnManager : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> objectsToRespawn = new List<GameObject>();

        private Queue<RespawnPoint> respawnPoints = new Queue<RespawnPoint>();
        private Queue<GameObject> queueObjtToRespaw = new Queue<GameObject>();

        private RespawnPoint currentSpawnPoint;
        private GameObject currentTarget;

        [SerializeField]
        private float timeToRespawn = 2f;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                respawnPoints.Enqueue(child.GetComponent<RespawnPoint>());               
            }

            for (int i = 0; i < objectsToRespawn.Count; i++)
            {
                queueObjtToRespaw.Enqueue(objectsToRespawn[i]);
            }

            currentSpawnPoint = respawnPoints.Dequeue();

            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(timeToRespawn);

            SpawnAt();

            UpdateSpawnPoint();

            StartCoroutine(Spawn());
        }

        private void SpawnAt()
        {
            currentTarget = queueObjtToRespaw.Dequeue();

            currentSpawnPoint.SetTarget(currentTarget);
            currentSpawnPoint.RespawnTarget();

            queueObjtToRespaw.Enqueue(currentTarget);
        }

        private void UpdateSpawnPoint()
        {
            respawnPoints.Enqueue(currentSpawnPoint);
            currentSpawnPoint = respawnPoints.Dequeue();
        }

        //Determinar a hora de spawnar
        //
        //Ativar o spawn point certo
        //
        //Escoher o objt a ser spawnado
        //
        //Mandar o spawn point instanciar o objt
        //
        //Dar update no spawn poit ativado
    }
}