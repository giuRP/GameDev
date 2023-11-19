using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.Common
{
    public class InstantiateUtil : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToInstantiate;

        public void CreateObject()
        {
            Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
        }
    }
}