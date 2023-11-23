using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth { get; protected set; }
    public float health { get; protected set; }
    public bool dead { get; protected set; }

    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        dead = true;
    }
    
    //체력회복
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health += newHealth;
    }
}
