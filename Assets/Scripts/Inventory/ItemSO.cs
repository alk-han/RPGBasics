using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public StatToChange statToChange = new();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new();
    public int amountToChangeAttribute;

    public Sprite sprite;
    public GameObject prefab;
    
    [TextArea]
    public string itemDescription;


    public bool UseItem()
    {
        if (statToChange == StatToChange.Health)
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            if (playerStatus.Health == playerStatus.MaxHealth)
            {
                return false;
            }
            else
            {
                playerStatus.RestoreHealth(amountToChangeStat);
                return true;
            }
        }
        else if (statToChange == StatToChange.Mana)
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            if (playerStatus.Mana == playerStatus.MaxMana)
            {
                return false;
            }
            else
            {
                playerStatus.RestoreMana(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        None,
        Health,
        Mana,
        Stamina
    };

    public enum AttributeToChange
    {
        None,
        Strength,
        Defense,
        Intelligence,
        Agility
    };
}
