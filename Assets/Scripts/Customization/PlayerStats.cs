using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the player's stats.
/// </summary>
[System.Serializable]
public class PlayerStats 
{
    [Header("Player Stats")]
    public float speed = 6f;
    public float sprintSpeed = 12f;
    public float crouchSpeed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
                    
    public int level;
    
    [Header("Current Stats")]
    private float currentHealth;
    public float maxHealth = 100f;
    public float regenHealth = 5f;
    public float currentMana = 100f;
    public float maxMana = 100f;
    public float currentStamina = 100f;
    public float maxStamina = 100f;

    public QuarterHearts healthHearts;

    [Header("Base Stats")]
    public int baseStatPoints = 10;
    public BaseStats[] baseStats;

    public float CurrentHealth 
    { 
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            
            if (healthHearts != null)
            {
                healthHearts.UpdateHearts(value, maxHealth);
            }
        }
    }

    public bool SetStats(int statIndex, int amount)
    {
        if (amount > 0 && baseStatPoints - amount < 0) // can't add point if there are none left
        {
            return false;
        }
        else if (amount < 0 && baseStats[statIndex].additionalStat + amount < 0) // we can't change default stats
        {
            return false;
        }

        // change stat
        baseStats[statIndex].additionalStat += amount;
        baseStatPoints -= amount;

        return true;
    }
}

[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat;
    public int levelUpStat;
    public int additionalStat;

    public int finalStat { get => defaultStat + additionalStat + levelUpStat; }
}
