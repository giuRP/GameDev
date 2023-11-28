using PI4.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.RespawnSystem
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager Instance { get; private set; }

        public List<RespawnHelper> respawnPoints = new List<RespawnHelper>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                respawnPoints.Add(transform.GetChild(i).GetComponent<RespawnHelper>());
            }
        }
    }
}