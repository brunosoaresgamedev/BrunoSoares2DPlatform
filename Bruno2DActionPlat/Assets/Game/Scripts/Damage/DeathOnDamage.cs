using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IDamageable
{

    public bool IsAlive { get; private set; } = true;

    public event Action OnDeath;

   
    public void TakeDamage(int damage)
    {
        Die();
        
    }
    private void Die()
    {
        if (IsAlive)
        {
            IsAlive = false;
            OnDeath?.Invoke();
        }
    }
}
