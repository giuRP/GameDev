using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    int _health, _maxHealth;

    bool isDead;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private void OnEnable()
    {
        isDead = false;
        _health = _maxHealth;
    }

    private void OnDisable()
    {
        isDead = true;
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }

        set
        {
            _maxHealth = value;
        }
    }

    public void InitializeHealth(int healthValue)
    {
        _health = healthValue;
        _maxHealth = healthValue;
    }

    public void Defeat()
    {
        gameObject.SetActive(false);
    }
    
    public void TakeDmg(int dmgAmount, GameObject sender)
    {
        if (isDead)
            return;

        _health -= dmgAmount;

        if(_health > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            isDead = true;
            OnDeathWithReference?.Invoke(sender);
        }
    }

    public void Heal(int healAmount)
    {
        if (_health >= _maxHealth)
            return;

        if(_health < _maxHealth)
        {
            _health += healAmount;
        }
    }
}
