using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public enum DamageType // Tipo di danno della Weapon
    {
        None = 0,
        Physical = 1,
        Magical = 2,
    }

    [SerializeField] private string name;
    public string Name // Inserisce nome della Weapon
    {
        get => name;
        set => name = value;
    }

    [SerializeField] private DamageType _damageType;
    public DamageType damageType //Iserisce il tipo di danno
    {
        get => _damageType;
        set => _damageType = value;
    }

    [SerializeField] private Element _element;
    public Element Element //Inserisce l'Element alla Weapon
    {
        get => _element;
        set => _element = value;
    }

    [SerializeField] private Stats _bonusStats;
    public Stats BonusStats // inserisce le statistische alla Weapon
    {
        get => _bonusStats;
        set => _bonusStats = value;
    }

    public Weapon(string nameWepon, DamageType damageType, Element element, Stats bonusStats) // THE Costuttore 
    {
        this.Name = nameWepon;
        this.damageType = damageType;
        this.Element = element;
        this.BonusStats = bonusStats;
    }
}
