using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public HealthController playerHealthController;

    public ResourcesController playerResourcesController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerHealthController.InitializeHealth(4);

        playerResourcesController.InitializeResources(100f, 100f, 6f, 6f);
    }
}
