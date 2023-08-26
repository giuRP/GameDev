using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIScaleElement : MonoBehaviour
    {
        private Sequence sequence;

        [SerializeField]
        private RectTransform uiElement;

        [SerializeField]
        private float animationEndScale;

        [SerializeField]
        private float animationTime;

        private float baseScaleValue;

        private Vector3 baseScale, endScale;

        [SerializeField]
        private bool playConstantly = false;

        private void Start()
        {
            baseScale = uiElement.localScale;
            endScale = Vector3.one * animationEndScale;

            if (playConstantly)
            {
                sequence = DOTween.Sequence()
                    .Append(uiElement.DOScale(baseScale, animationTime))
                    .Append(uiElement.DOScale(endScale, animationTime));
                sequence.SetLoops(-1, LoopType.Yoyo);
                sequence.Play();
            }
        }

        public void PlayAnimation()
        {
            StopAllCoroutines();
            uiElement.localScale = baseScale;
            StartCoroutine(ScaleImage(true));
        }

        private void OnDestroy()
        {
            if (sequence != null)
                sequence.Kill();
        }

        public IEnumerator ScaleImage(bool scaleUp)
        {
            float time = 0;
            while (time < animationTime)
            {
                time += Time.deltaTime;
                float value = (time / animationTime);
                Vector3 currentScale;
                if (scaleUp)
                {
                    currentScale = baseScale + value * (endScale - baseScale);
                }
                else
                {
                    currentScale = endScale - value * (endScale - baseScale);
                }
                uiElement.localScale = currentScale;
                yield return null;
            }

            uiElement.localScale = scaleUp ? endScale : baseScale;
            if (playConstantly || scaleUp == true)
                StartCoroutine(ScaleImage(!scaleUp));
        }
    }
}