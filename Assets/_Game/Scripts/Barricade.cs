using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private float health;
    public float maxHealth;
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        GameEvents.BarricadeTakeDamageEvent?.Invoke();
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
