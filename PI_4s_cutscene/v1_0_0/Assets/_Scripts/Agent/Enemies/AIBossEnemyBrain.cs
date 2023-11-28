using PI4.RespawnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.AI
{
    public class AIBossEnemyBrain : AIEnemyInput
    {
        public AIBehaviour moveBehaviour;

        BossStages currentStage;

        [SerializeField]
        private Agent agent;
        private Damageable damageable;

        private void Awake()
        {
            agent = GetComponentInChildren<Agent>();
            agent.data.maxSpeed = 3;

            damageable = agent.GetComponent<Damageable>();

            GameManager.Instance.bossEnemy = agent;
        }

        private void Start()
        {
            GameManager.Instance.OnFirstPartBossFight += FirstBossFightBehaviours;
        }

        private void Update()
        {
            switch (currentStage)
            { 
                default:
                    break;
            }
        }

        private void FirstBossFightBehaviours()
        {
            Debug.Log("A PRIMEIRA LUTA COMEÇOU");
            moveBehaviour.PerformAction(this);

            RespawnManager.Instance.respawnPoints[1].RespawnRequest();
            RespawnManager.Instance.respawnPoints[2].RespawnRequest();
        }
        
        private void HandleFinishFirstBossFight()
        {
            Debug.Log("A LUTA ESTÁ PAUSADA");
        }

        private void SecondBossFightBehaviours()
        {
            Debug.Log("A SEGUNDA LUTA COMEÇOU");
        }
    }

    enum BossStages
    {
        Stage0,
        Stage1,
        Stage2,
        Stage3
    }
}