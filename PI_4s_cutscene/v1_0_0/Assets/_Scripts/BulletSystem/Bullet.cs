using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PI4.BulletSystem
{
    public abstract class Bullet : MonoBehaviour
    {
        public bool isInitialized = false;

        public BulletData data;
        public Rigidbody2D rb2d;

        public Vector2 startPosition = Vector2.zero;
        public Vector2 movementDirection;

        public LayerMask hittableLayerMask;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        public void Initialize(BulletData data, Vector2 shootDirection, LayerMask mask)
        {
            this.data = data;
            hittableLayerMask = mask;
            movementDirection = shootDirection;
            rb2d.gravityScale = data.gravityScale;
            rb2d.velocity = movementDirection * data.xSpeed;
            isInitialized = true;
        }

        private void Update()
        {
            if (isInitialized)
            {
                PerformAttack();

                if (((Vector2)transform.position - startPosition).magnitude >= data.range)
                {
                    Destroy(gameObject);
                }
            }
        }

        public virtual void BulletUpdate()
        {
            if (isInitialized)
            {
                PerformAttack();

                if (((Vector2)transform.position - startPosition).magnitude >= data.range)
                {
                    Destroy(gameObject);
                }
            }
        }

        public abstract void PerformAttack();
    }
}