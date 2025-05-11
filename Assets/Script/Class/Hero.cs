using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class Hero
{
    [SerializeField] private string _name;
    public string Name //inserisce nome Hero
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField] private int _hp;
    public int Hp // inserisce HP Hero
    {
        get => _hp;
        set => _hp = Mathf.Max(0,value);
    }

    [SerializeField] private Stats _baseStats;
    public Stats BaseStats //inserisce statistiche Hero
    {
        get => _baseStats;
        set => _baseStats = value;
    }

    [SerializeField] private Element _resistance;
    public Element Resistance //inserisce la resistenza all'elemento
    {
        get => _resistance;
        set => _resistance = value;
    }

    [SerializeField] private Element _weakness;
    public Element Weakness //inserisce la debolezza all'elemento
    {
        get => _weakness;
        set => _weakness = value;
    }

    [SerializeField] private Weapon _weapon;
    public Weapon Weapon // inserisce che tipo di Weapon ha
    {
        get => _weapon;
        set => _weapon = value;
    }

    public Hero(string name, int hp, Stats baseStats, Element resistance, Element weakness, Weapon weapon) // THE Costruttore
    {
        this.Name = name;
        this.Hp = hp;
        this.BaseStats = baseStats;
        this.Resistance = resistance;
        this.Weakness = weakness;
        this.Weapon = weapon;
    }

    public void AddHP(int amount) // Mettiamo Gli Hp sia positivi sia negativi al player 
    {
        //Debug.Log("Set Nuovi HP " +  amount);
        Hp += amount;
    }

    public void TakeDamage(int damage) //Prendiamo danno quindi si strasforma il numero positivo in un numero negativo
    {
        AddHP(-damage);
    }

    public bool IsAlive() // Si controlla che Hero sia ancora vivo, Si se a almeno più di 1HP
    {
        if (Hp > 0)
        {
            return true;
        }
        else {return false; }
    }
}
