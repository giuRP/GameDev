using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PI4.BulletSystem
{
    public class BulletStorage : MonoBehaviour
    {
        public static BulletStorage Instance;

        public List<BulletData> bulletDataList = new List<BulletData>();

        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < bulletDataList.Count; i++)
            {
                bulletDataList[i].ID = i;
            }
        }

        private void Start()
        {
            
        }
    }
}