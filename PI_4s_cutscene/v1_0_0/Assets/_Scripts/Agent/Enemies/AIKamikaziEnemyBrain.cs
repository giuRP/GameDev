using PI4.AI;
using PI4.BulletSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AIKamikaziEnemyBrain : AIEnemyInput
{
    public Behaviour chaseBehaviour;

    [SerializeField]
    private Agent agent;

    [SerializeField]
    private PlayerDetector playerDetector;

    [SerializeField]
    private LineRenderer lineRenderer;

    Vector3 lastPlayerPosition = Vector3.zero;

    private void Awake()
    {
        agent = GetComponentInChildren<Agent>();
        playerDetector = GetComponentInChildren<PlayerDetector>();

        lineRenderer = agent.GetComponent<LineRenderer>();       
    }

    private void Start()
    {
        lineRenderer.enabled = false;

        StartCoroutine(LockTargetCoroutine());        
    }

    private void RotateSprite()
    {
        agent.transform.right = playerDetector.Target.transform.position - agent.transform.position ;
    }

    public void AplyDamageToTarget()
    {
        if (playerDetector.Target == null)
            return;

        foreach (var hittable in playerDetector.Target.GetComponents<IHittable>())
        {
            hittable.GetHit(gameObject, 1);
        }
    }

    private void EnableLineRenderer()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, agent.transform.position);

        lineRenderer.SetPosition(1, lastPlayerPosition);
    }

    IEnumerator LockTargetCoroutine()
    {
        playerDetector.OnDetectedTarget += RotateSprite;
        yield return new WaitForSeconds(2);
        playerDetector.OnDetectedTarget -= RotateSprite;

        if (playerDetector.PlayerDetected)
        {
            lastPlayerPosition = playerDetector.Target.transform.position;

            StartCoroutine(ChaseCoroutine());
        }
        else
        {
            StartCoroutine(LockTargetCoroutine());
        }
    }
    
    IEnumerator ChaseCoroutine()
    {   
        EnableLineRenderer();
        yield return new WaitForSeconds(1);

        MovementDirection = (lastPlayerPosition - agent.transform.position).normalized;
        CallOnMovement(MovementDirection);
        yield return new WaitForSeconds(2);
        lineRenderer.enabled = false;
        MovementDirection = Vector2.zero;

        StartCoroutine(LockTargetCoroutine());
    }
}
