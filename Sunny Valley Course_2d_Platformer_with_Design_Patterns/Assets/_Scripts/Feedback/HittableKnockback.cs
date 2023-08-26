using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FeedbackSystem
{
    public class HittableKnockback : MonoBehaviour, IHittable
    {
        [SerializeField]
        private Rigidbody2D rb2d;

        [SerializeField]
        private float force = 10f;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void GetHit(GameObject sender, int damage)
        {
            Vector2 direction = transform.position - sender.transform.position;
            rb2d.AddForce(new Vector2(direction.normalized.x, 0) * force, ForceMode2D.Impulse);
        }
    }
}