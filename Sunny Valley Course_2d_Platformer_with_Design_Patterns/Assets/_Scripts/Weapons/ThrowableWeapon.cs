using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class ThrowableWeapon : MonoBehaviour
    {
        RangeWeaponData data;
        Rigidbody2D rb2d;

        Vector2 startPosition = Vector2.zero;
        Vector2 movementDirection;

        bool isInitialized = false;

        public Transform spriteTransform;

        public float rotationRate = 1;

        [Header("Collision Detection Data")]
        [SerializeField]
        private Vector2 center = Vector2.zero;
        [SerializeField]
        [Range(0.1f, 2f)]
        private float radius = 1;
        [SerializeField]
        private Color gizmoColor = Color.red;
        private LayerMask layerMask;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (spriteTransform == null)
                spriteTransform = transform.GetChild(0);
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        public void Initialize(RangeWeaponData data, Vector2 direction, LayerMask mask)
        {
            this.data = data;
            this.movementDirection = direction;
            layerMask = mask;
            isInitialized = true;
            rb2d.velocity = movementDirection * data.weaponThrowSpeed;
        }

        private void Update()
        {
            if (isInitialized)
            {
                Fly();
                DetectCollision();

                if (((Vector2)transform.position - startPosition).magnitude >= data.attackRange)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void DetectCollision()
        {
            Collider2D collision = Physics2D.OverlapCircle((Vector2)transform.position + center, radius, layerMask);
            if (collision != null)
            {
                foreach (var hittable in collision.GetComponents<IHittable>())
                {
                    hittable.GetHit(gameObject, data.weaponDamage);
                }
                Destroy(gameObject);
            }
        }

        private void Fly()
        {
            spriteTransform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rotationRate * -movementDirection.x);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position + (Vector3)center, radius);
        }
    }
}