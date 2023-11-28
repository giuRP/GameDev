using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.UI
{
    public class UIInfinityScrollElement : MonoBehaviour
    {
        public Canvas canvas;

        public float speed = 100;
        public float outsideScreenDistance;

        private Vector3 startPosition = Vector2.zero;

        private void Awake()
        {
            canvas = transform.parent.GetComponentInParent<Canvas>();
        }

        private void Start()
        {
            startPosition = transform.position;
            //Debug.Log(startPosition);
        }

        private void Update()
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
            float distance = transform.position.x - startPosition.x;
            //Debug.Log(distance);

            if(distance * canvas.scaleFactor < outsideScreenDistance)
            {
                ResetPosition();
            }
        }

        private void ResetPosition()
        {
            transform.position = new Vector3(2880, 540, 0);
        }
    }
}