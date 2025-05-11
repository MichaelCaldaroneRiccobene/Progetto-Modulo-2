using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [Header("Condizioni")]// Decido le Condizioni Della Battaglia 
    public bool isPrestabilito = true;
    public bool isAutomatic = true;

    [Header("Eroi")] // Creo i miei Hero
    public Hero heroA;
    public Hero heroB;

    private Hero hero1; // Determina chi attacca per primo
    private Hero hero2; // Determina chi attacca per secondo

    private void Start()
    {
        if (isPrestabilito) //Si mettono già i settaggi per le Wepons e Heros, si puo anche non fare
        {
            Weapon weponA = new Weapon("Thunger", Weapon.DamageType.Physical, Element.Fire, new Stats(22, 13, 4, 18, 13, 27, 17));
            Weapon weponB = new Weapon("Loca", Weapon.DamageType.Magical, Element.Lightning, new Stats(22, 11, 4, 18, 13, 27, 17));

            heroA = new Hero("Giggino", 150, new Stats(20, 13, 4, 20, 15, 27, 17), Element.Fire, Element.Ice, weponA);
            heroB = new Hero("Jim", 150, new Stats(20, 13, 4, 18, 13, 27, 17), Element.Lightning, Element.Fire, weponB);
        }
        // Si sommano le statistiche dell'Hero con le statistiche dell wepon
        Stats statsCombo1 = Stats.Sum(heroA.BaseStats, heroA.Weapon.BonusStats); 
        Stats statsCombo2 = Stats.Sum(heroB.BaseStats, heroB.Weapon.BonusStats);

        //Decide chi attacca per primo
        AttackFirst(statsCombo1, statsCombo2, heroA, heroB);

        //Controllo Se uno dei due Hero hanno 0 Hp all'inizio
        if (!heroA.IsAlive() || !heroB.IsAlive())
        {
            Debug.Log("Uno o tutti gli Hero hanno 0 HP"); enabled = false;
        }
    }

    private void Update()
    {
        // Avviene L'attacco in modo automatico o manuale
        if (isAutomatic) { Attacco(hero1, hero2); } 
        else if (Input.GetKeyDown(KeyCode.Space)) { Attacco(hero1, hero2); }
    }

    void AttackFirst(Stats a, Stats b, Hero z, Hero y)
    {
        if (a.spd >= b.spd)
        {
            hero1 = z; hero2 = y;
        }
        else
        {
            hero1 = y; hero2 = z;
        }
    }
    void Attacco(Hero a, Hero b)
    {
        // Inizio Attacco 
        GoAttack(a, b);

        // Secondo Attacco
        GoAttack(b, a);
    }
    void GoAttack(Hero a, Hero b)
    {
        // Controlla che tutti e due siano vivi prima di fare il codice 
        if (a.IsAlive() && b.IsAlive())
        {
            Debug.Log("Attaccante " + a.Name + " Attacca con elemento " + a.Weapon.Element + " Difensore " + b.Name + " Debolezza " + b.Weakness);

            Stats statsCombo1 = Stats.Sum(a.BaseStats, a.Weapon.BonusStats);
            Stats statsCombo2 = Stats.Sum(b.BaseStats, b.Weapon.BonusStats);

            //Solo se colpice da il danno
            if (GameFormulas.HasHit(statsCombo1, statsCombo2))
            {
                //Calcola il danno base che L'attaccante dovra fare al Difensore
                int danno = (GameFormulas.CalculateDamage(a, b));

                //Vediamo se il difensore è Debole o restistente
                if (GameFormulas.HasElementAdvantage(a.Weapon.Element, b)) { Debug.Log("Weak"); }
                if (GameFormulas.HasElementDisadvantage(a.Weapon.Element, b)) { Debug.Log("Resisten"); }

                b.TakeDamage(danno);
                Debug.Log("Danno Attaccante " + danno + " Vita difensore " + b.Hp);
            }
            //Il difensore non ha più il danno quindi l'attaccante vince 
            if (!b.IsAlive())
            {
                Debug.Log(" Vincitore " + a.Name);
                enabled = false;
            }
        }
    }
}
