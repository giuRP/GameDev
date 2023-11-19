using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.UI
{
    public class UIShakeElement : MonoBehaviour
    {
        public RectTransform element;

        [Header("Shake Animation Settings")]
        public float animationTime = 0.5f, shakeStrength = 90, randomness = 90;
        public int vibrato = 90;
        public bool fadeOut = true;
        public float delayBetweenShakes = 3;

        Sequence sequence;

        private void Start()
        {
            sequence = DOTween.Sequence()
                .Append(element.DOShakeRotation(animationTime, shakeStrength, vibrato, randomness, fadeOut));
            sequence.SetLoops(-1, LoopType.Restart);
            sequence.AppendInterval(delayBetweenShakes);
            sequence.Play();
        }

        private void OnDestroy()
        {
            sequence.Kill();
        }
    }
}