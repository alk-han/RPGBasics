using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth  = 100;
    [SerializeField] private int health     = 50;
    [SerializeField] private int maxMana    = 100;
    [SerializeField] private int mana       = 20;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int stamina    = 100;
    public event Action<int, PlayerStatusType> OnPlayerStatusChange;


    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int Health 
    { 
        get { return health; }
        set 
        {
            health = Mathf.Clamp(value, health, maxHealth);
            OnPlayerStatusChange.Invoke(health, PlayerStatusType.Health);
        } 
    }

    public int MaxStamina
    {
        get { return maxStamina; }
    }

    public int Stamina 
    { 
        get { return stamina; }
        set 
        {
            stamina = Mathf.Clamp(value, stamina, maxStamina);
            OnPlayerStatusChange.Invoke(stamina, PlayerStatusType.Stamina);
        } 
    }

    public int MaxMana
    {
        get { return maxMana; }
    }

    public int Mana 
    { 
        get { return mana; }
        set 
        {
            mana = Mathf.Clamp(value, mana, maxMana);
            OnPlayerStatusChange.Invoke(mana, PlayerStatusType.Mana);
        } 
    }

    public void RestoreHealth(int amount)
    {
        Health += amount;
    }

    public void RestoreMana(int amount)
    {
        Mana += amount;
    }

    public enum PlayerStatusType
    {
        Health,
        Mana,
        Stamina
    }
}
