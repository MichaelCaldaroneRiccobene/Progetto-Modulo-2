using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero
{

    [SerializeField] private string _name;
    public string name //inserisce nome Hero
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField] private int _hp;
    public int hp // inserisce HP Hero
    {
        get => _hp;
        set => _hp = value;
    }

    [SerializeField] private Stats _baseStats;
    public Stats baseStats //inserisce statistiche Hero
    {
        get => _baseStats;
        set => _baseStats = value;
    }

    [SerializeField] private Element _resistance;
    public Element resistance //inserisce la resistenza all'elemento
    {
        get => _resistance;
        set => _resistance = value;
    }

    [SerializeField] private Element _weakness;
    public Element weakness //inserisce la debolezza all'elemento
    {
        get => _weakness;
        set => _weakness = value;
    }

    [SerializeField] private Weapon _weapon;
    public Weapon weapon // inserisce che tipo di weapon ha
    {
        get => _weapon;
        set => _weapon = value;
    }


    public Hero(string nameHero, int hpHero, Stats basicStats, Element resistence, Element weakness, Weapon wepon) // THE Costruttore
    {
        _name = nameHero;
        _hp = SetHP(hpHero);
        _baseStats = basicStats;
        _resistance = resistence;
        _weakness = weakness;
        _weapon = wepon;
    }

    public void AddHP(int amount) // Mettiamo Gli Hp sia positivi sia negativi al player 
    {
        SetHP(_hp += amount);
    }

    public void TakeDamage(int damage) //Prendiamo danno quindi si strasforma il numero positivo in un numero negativo
    {
        AddHP(-damage);
    }
    public bool IsAlive() // Si controlla che Hero sia ancora vivo, Si se a almeno più di 1HP
    {
        if (_hp > 0)
        {
            return true;
        }
        else { return false; }
    }
    int SetHP(int amount) //Prendiamo Gli amount di AddHp e li mettiamo se positivi (100 + 5 = 105) se negativi (100-5 = 95)
    {
        _hp = amount;
        if (_hp < 0) // controlliamo se scende sotto lo zero, se si impostiamo manualmente noi gli HP e li si mette a 0
        {
            _hp = 0;
        }
        return _hp;
    }


}
