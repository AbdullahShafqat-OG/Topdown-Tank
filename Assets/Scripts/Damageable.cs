using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _health;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChange?.Invoke((float)Health / _maxHealth);
        }
    }

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;

    private void Start()
    {
        Health = _maxHealth;
    }

    internal void Hit(int damagePoints)
    {
        Health -= damagePoints;
        if (Health <= 0)
            OnDead?.Invoke();
        else
            OnHit?.Invoke();
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        OnHeal?.Invoke();
    }
}
