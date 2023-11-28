using PI4.RespawnSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Agent bossEnemy;

    public event Action OnFirstPartBossFight, OnFinishFirstBossFight, OnSecondPartBossFight;

    [Header("Tutorial parameters")]
    public SpriteRenderer sr;
    public Sprite tutorial1;
    public Sprite tutorial2;
    public float tutorialDelay = 0f;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(StartGameFlowCoroutine());
    }

    private void Update()
    {
        if (bossEnemy == null)
            return;
    }

    IEnumerator StartGameFlowCoroutine()
    {
        //Tutorial
        yield return new WaitForSeconds(1);
        sr.sprite = tutorial1;
        yield return new WaitForSeconds(tutorialDelay);
        sr.sprite = tutorial2;
        yield return new WaitForSeconds(tutorialDelay);
        sr.enabled = false;

        RespawnManager.Instance.respawnPoints[0].RespawnRequest();
        yield return new WaitForSeconds(1);
        OnFirstPartBossFight?.Invoke();
    }

    IEnumerator WaitToStartSecondBossFight()
    {
        yield return new WaitForSeconds(20);
        OnSecondPartBossFight?.Invoke();
    }
}
