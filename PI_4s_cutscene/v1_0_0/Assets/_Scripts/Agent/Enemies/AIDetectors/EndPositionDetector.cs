using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPositionDetector : MonoBehaviour
{
    [SerializeField]
    private Agent agent;

    [SerializeField]
    private Vector2 startPosition, endPosition, targetPosition;

    private float xEndPosition;

    public event Action OnArrivedAtStartPosition;
    public event Action OnArrivedAtEndPosition;

    private void Awake()
    {
        if(agent != null)
        {
            agent = GetComponent<Agent>();
        }
    }

    private void Start()
    {
        startPosition = agent.transform.position;

        endPosition = ChoseNewEndPosition();

        targetPosition = endPosition;
    }

    private void Update()
    {
        float distance = Mathf.Abs(targetPosition.x - agent.transform.position.x);

        if(distance <= .1f)
        {
            CheckTargetPosition();
        }

        //Debug.Log(distance);
    }

    private void CheckTargetPosition()
    {
        if(targetPosition == endPosition)
        {
            targetPosition = startPosition;
            OnArrivedAtEndPosition?.Invoke();
        }
        else
        {
            targetPosition = endPosition;
            OnArrivedAtStartPosition?.Invoke();
            ChoseNewEndPosition();
        }
    }

    private Vector2 ChoseNewEndPosition()
    {
        xEndPosition = UnityEngine.Random.Range(1, 4.5f);

        return new Vector2(xEndPosition, 0);
    }
}
