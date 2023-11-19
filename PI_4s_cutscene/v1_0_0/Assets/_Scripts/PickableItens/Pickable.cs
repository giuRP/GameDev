using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.PickableSystem
{
    public abstract class Pickable : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;

        [SerializeField]
        private BoxCollider2D pickableCollider;

        [SerializeField]
        private Rigidbody2D rb2d;

        [SerializeField]
        [Range(-10, 10)]
        private float scrollSpeed = -1;

        [SerializeField]
        protected Color gizmoColor = Color.magenta;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            pickableCollider = GetComponent<BoxCollider2D>();
            rb2d = GetComponent<Rigidbody2D>();

            rb2d.velocity = new Vector2(scrollSpeed, 0);
        }

        public abstract void PickUp(Agent agent);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PickUp(collision.GetComponent<Agent>());
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(pickableCollider.bounds.center, pickableCollider.bounds.size);
        }
    }
}