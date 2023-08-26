using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FeedbackSystem
{
    public class FlashWhiteFeedback : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float feedbackDuration = 0.1f;

        public void PlayFeedback()
        {
            if (spriteRenderer == null || spriteRenderer.material.HasProperty("_MakeSolidColor") == false)
                return;

            ToggleMaterial(1);

            StopAllCoroutines();

            StartCoroutine(ResetColor());
        }

        private void ToggleMaterial(int value)
        {
            value = Mathf.Clamp(value, 0, 1);
            spriteRenderer.material.SetInt("_MakeSolidColor", value);
        }

        IEnumerator ResetColor()
        {
            yield return new WaitForSeconds(feedbackDuration);
            ToggleMaterial(0);
        }
    }
}