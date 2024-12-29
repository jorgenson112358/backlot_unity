using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public int healthPotCount;

    public PlayerData(Huntress p)
    {
        currentHealth = p.currentHealth;
        healthPotCount = p.GetHealthPotCount();
    }
}
