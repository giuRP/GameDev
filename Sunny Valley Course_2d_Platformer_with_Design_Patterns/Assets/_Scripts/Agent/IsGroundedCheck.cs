using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsGroundedCheck : MonoBehaviour
{
    [SerializeField]
    private GroundDetector groundDetector;

    public UnityEvent OnConditionValidAction;

    public void TryPerformAction()
    {
        if (groundDetector.isGrounded)
        {
            OnConditionValidAction?.Invoke();
        }
    }
}
