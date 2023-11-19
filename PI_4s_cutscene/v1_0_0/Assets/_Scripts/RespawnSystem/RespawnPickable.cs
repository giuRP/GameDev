using PI4.PickableSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPickable : MonoBehaviour
{
    [SerializeField]
    private List<Pickable> pickables = new List<Pickable>();

    [SerializeField]
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    
}
