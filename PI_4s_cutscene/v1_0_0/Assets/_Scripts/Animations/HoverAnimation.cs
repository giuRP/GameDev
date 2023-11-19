using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PI4.Animations
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

            //transform
            //    .DOLocalRotate(new Vector3(0, 180, 0), animationDuration)
            //    .SetEase(animationEase)
            //    .SetLoops(-1);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}