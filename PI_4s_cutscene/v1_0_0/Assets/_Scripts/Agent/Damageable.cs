using PI4.BulletSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IHittable
{
    [SerializeField]
    private int maxHealth, currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;

        set
        {
            currentHealth = value;
            OnHealthValueChange?.Invoke(currentHealth);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public UnityEvent OnGetHit;
    public UnityEvent OnDie;
    public UnityEvent OnAddHealth;

    public UnityEvent<int> OnHealthValueChange;

    public UnityEvent<int> OnInitializeMaxHealth;

    public void GetHit(GameObject sender, int damage)
    {
        Hit(damage);
    }

    public void Hit(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            OnGetHit?.Invoke();
        }
    }

    public void Heal(int heal)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, maxHealth);
        OnAddHealth?.Invoke();
    }

    public void InitializeHealth(int health)
    {
        maxHealth = health;
        OnInitializeMaxHealth?.Invoke(maxHealth);
        CurrentHealth = maxHealth;
    }
}
