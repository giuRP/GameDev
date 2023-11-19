using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.Common
{
    public class DestroyUtil : MonoBehaviour
    {
        public void DestroySelf()
        {
            DestroyObject(gameObject);
        }

        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
    }
}