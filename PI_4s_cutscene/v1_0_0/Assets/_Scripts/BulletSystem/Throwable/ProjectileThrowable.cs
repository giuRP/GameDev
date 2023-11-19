using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PI4.BulletSystem
{
    public class ProjectileThrowable : Throwable
    {
        Rigidbody2D rb2d;
        Animator anim;

        Vector2 startPosition = Vector2.zero;
        Vector2 direction;

        LayerMask layerMask;

        bool canAttack = false;

        [Header("Collision Detection Data")]
        [SerializeField]
        private Vector2 center = Vector2.zero;
        [SerializeField]
        [Range(0.1f, 2f)]
        private float radius = 1;
        [SerializeField]
        private Color gizmoColor = Color.red;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public override void Initialize(_BulletData data, LayerMask hittableLayerMask, Vector2 shootDirection)
        {
            isInitialize = true;

            bulletData = data;
            layerMask = hittableLayerMask;
            direction = shootDirection;
        }    

        public override void EnableBullet()
        {
            rb2d.velocity = direction * bulletData.throwSpeed;
            anim.Play("Attack", -1, 0f);
            OnEnableBullet?.Invoke();

            canAttack = true;
        }

        private void Update()
        {
            if (canAttack == false)
                return;

            UpdateBullet();
        }

        public override void UpdateBullet()
        {
            Collider2D collision = Physics2D.OverlapCircle((Vector2)transform.position + center, radius, layerMask);
            if (collision != null)
            {
                foreach (var hittable in collision.GetComponents<IHittable>())
                {
                    hittable.GetHit(gameObject, bulletData.damage);
                }
                DisableBullet();
            }

            if (((Vector2)transform.position - startPosition).magnitude >= bulletData.range)
            {
                Destroy(gameObject);
            }
        }

        public override void DisableBullet()
        {
            OnDisableBullet?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position + (Vector3)center, radius);
        }
    }
}