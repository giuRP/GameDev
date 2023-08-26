using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FeedbackSystem
{
    public class HittableTempImmortality : MonoBehaviour, IHittable
    {
        public Collider2D[] colliders = new Collider2D[0];

        [SerializeField]
        private float immortalityDuration = 1;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float flashDelay = 0.1f;

        [SerializeField]
        [Range(0, 1)]
        private float flashAlpha = 0.5f;

        [Header("Debbug")]
        public bool isImmortal = false;

        private void Awake()
        {
            if (colliders.Length == 0)
                colliders = GetComponents<Collider2D>();
        }

        public void GetHit(GameObject sender, int damage)
        {
            if (this.enabled == false)
                return;

            ToggleColliders(false);
            StartCoroutine(ResetColliders());
            StartCoroutine(Flash(flashAlpha));
        }

        private void ToggleColliders(bool value)
        {
            isImmortal = !value;
            foreach (var collider in colliders)
            {
                collider.enabled = value;
            }
        }

        private void ChangeSpriteRendererColorAlpha(float alpha)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }

        IEnumerator ResetColliders()
        {
            yield return new WaitForSeconds(immortalityDuration);
            StopAllCoroutines();
            ToggleColliders(true);
            ChangeSpriteRendererColorAlpha(1);
        }

        IEnumerator Flash(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            ChangeSpriteRendererColorAlpha(alpha);
            yield return new WaitForSeconds(flashDelay);
            StartCoroutine(Flash(alpha < 1 ? 1 : flashAlpha));
        }
    }
}