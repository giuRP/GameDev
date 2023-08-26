using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class FragileBlock : MonoBehaviour, IHittable
{
    public UnityEvent OnHit;

    public void GetHit(GameObject sender, int damage)
    {
        OnHit?.Invoke();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
