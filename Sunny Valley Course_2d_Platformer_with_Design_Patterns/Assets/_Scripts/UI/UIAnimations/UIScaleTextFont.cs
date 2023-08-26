using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIScaleTextFont : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private float fontAnimationSize = 80, fontAnimationDuration = 0.3f;

        private float baseFontSize;

        private void Awake()
        {
            baseFontSize = text.fontSize;
        }

        public void PlayAnimation()
        {
            StopAllCoroutines();
            text.fontSize = baseFontSize;
            StartCoroutine(AnimateText(fontAnimationDuration));
        }

        IEnumerator AnimateText(float duration)
        {
            float time = 0;

            float delta = fontAnimationSize - baseFontSize;

            while (time < duration)
            {
                time += Time.deltaTime;
                float newFontSize = baseFontSize + delta * (time / duration);
                text.fontSize = newFontSize;
                yield return null;
            }
            text.fontSize = baseFontSize;
        }
    }
}