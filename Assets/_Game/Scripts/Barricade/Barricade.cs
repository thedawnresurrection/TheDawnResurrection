using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public List<SOBarricadeData> barricadeData;
    private float health;
    private float armor;

    private float maxHealth;
    private int currentLevel;

    private float armorRating;
    private void Start()
    {
        ChangeLevel(0);
    }

    
    public void ChangeLevel(int newLevel)
    {
        maxHealth = barricadeData[newLevel].maxHealth;
        armor = barricadeData[newLevel].maxArmor;   
        armorRating = barricadeData[newLevel].armorRating;
        currentLevel = barricadeData[newLevel].barricadeLevel;
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        float effectiveDamage = damage *(1- armorRating);
        if (armor>0)
        {
            float armorDamage = Mathf.Min(armor, effectiveDamage);
            armor -= armorDamage;
            effectiveDamage -= armor;
        }

        if (effectiveDamage > 0)
        {
            health -= effectiveDamage;
        }

        GameEvents.BarricadeTakeDamageEvent?.Invoke();
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
