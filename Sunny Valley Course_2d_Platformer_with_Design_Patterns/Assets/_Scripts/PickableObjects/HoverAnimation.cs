using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PickableSystem
{
    public class HoverAnimation : MonoBehaviour
    {
        public float movementDistance = 0.5f;
        public float animationDuration = 1;
        public Ease animationEase;

        private void Start()
        {
            transform
                .DOMoveY(transform.position.y + movementDistance, animationDuration)
                .SetEase(animationEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}