using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.RespawnSystem
{
    public class RespawnHelper : MonoBehaviour
    {
        private RespawnManager manager;

        private void Awake()
        {
            manager = FindObjectOfType<RespawnManager>();
        }

        private void OnEnable()
        {
            
        }

        public void RespawnAgent()
        {
            
        }

        private void OnDestroy()
        {
            
        }
    }
}