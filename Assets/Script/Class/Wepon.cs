using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wepon
{
    public enum DamageType // Tipo di danno della Wepon
    {
        None = 0,
        Physical = 1,
        Magical = 2,
    }

    [SerializeField] private string _name;
    public string name // Inserisce nome della Wepon
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField] private DamageType _damageType;
    public DamageType damageType //Iserisce il tipo di danno
    {
        get => _damageType;
        set => _damageType = value;
    }

    [SerializeField] private Element _element;
    public Element element //Inserisce l'element alla wepon
    {
        get => _element;
        set => _element = value;
    }

    [SerializeField] private Stats _bonusStats; 
    public Stats bonusStats // inserisce le statistische alla wepon
    {
        get => _bonusStats;
        set => _bonusStats = value;
    }


    public Wepon(string nameWepon, DamageType damageType, Element element, Stats bonusStats) // THE Costuttore 
    {
        _name = nameWepon;
        _damageType = damageType;
        _element = element;
        _bonusStats = bonusStats;
    }
}
