using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFormulas
{
    // Prende Elemento Spada, e debolezza Hero
    public static bool HasElementAdvantage(Element attkElement, Hero defender)
    {
        if (attkElement == defender.Weakness)
        {
            return true;
        }
        else { return false; }
    }

    // Prende Elemento Spada, e resistenza Hero
    public static bool HasElementDisadvantage(Element attkElement, Hero defender)
    {
        if (attkElement == defender.Resistance)
        {
            return true;
        }
        else { return false; }
    }

    //Guarda se bisogna moltiplicare o dimezzare il danno
    public static float EvaluateElementModifier(Element attkElement, Hero defender)
    {
        if (HasElementAdvantage(attkElement, defender)) return 1.5f;
        if (HasElementDisadvantage(attkElement, defender)) return 0.5f;
        return 1;
    }

    //Controlla se si colpisce l'altro Hero 
    public static bool HasHit(Stats attacker, Stats defender)
    {
        float hitChance = attacker.aim - defender.eva;

        float randomaChance = UnityEngine.Random.Range(0, 100);
        //Debug.Log("Attaker Aim " + attacker.aim + " Defender Eva " + defender.eva + " Hit Chance " + hitChance + " RandomChance " + randomaChance);
        // Es HitChance e 60 > a Es randomChache 55 allora si colpisce se no niente 
        if (hitChance > randomaChance) 
        {
            return true;
        }
        else
        {
            Debug.Log("MISS");
            return false;
        }
    }

    // Controlla se si fa un critico 
    public static bool IsCrit(int criticValue)
    {
        float randomaChance = UnityEngine.Random.Range(0, 100);
        //Debug.Log("Critico " +  criticValue + " Possibilita " + randomaChance);

        if (criticValue > randomaChance)
        {
            Debug.Log("CRIT");
            return true;
        }
        else
        {
            return false;
        }
    }

    // Si calcola il Danno
    public static int CalculateDamage(Hero attacker, Hero defender) 
    {
        //Statistiche degli Hero sommati alle Weapon
        Stats newStatsAttacker = Stats.Sum(attacker.BaseStats, attacker.Weapon.BonusStats);
        Stats newStatsDefender = Stats.Sum(defender.BaseStats, defender.Weapon.BonusStats);

        // Che danno si applica e come ci si difende
        float risultatoAttaco = AttMenoDif(attacker.Weapon.damageType, newStatsAttacker, newStatsDefender);
        //Debug.Log(" Riusultato Danno " + risultatoAttaco);

        //Il danno viene radoppiato o dimezzato o rimane normale 
        float dannoElemet = EvaluateElementModifier(attacker.Weapon.Element, defender);
        risultatoAttaco *= dannoElemet;

        // vediamo se è un critico 
        bool isCrit = IsCrit(newStatsAttacker.crt);

        // se si si radoppia 
        if (isCrit) { risultatoAttaco *= 2; }
        //se è minore di 0 si mette 0
        if (risultatoAttaco < 0) { return 0; } 

        int risulAttck = Mathf.FloorToInt(risultatoAttaco);
        //Si riporta il risultato dell'Attacco
        return risulAttck; 
    }

    // Funzione che vede come applicare il danno guardando come attacca l'attacante e come deve rispondere il difensore
    static float AttMenoDif(Weapon.DamageType damage, Stats attk, Stats def) 
    {
        if (damage == Weapon.DamageType.Physical)
        {
            //Debug.Log("Difesa Utilizzata Def " + "Attaco " + attk.atk + " Difesa " + def.def);
            return attk.atk - def.def;
        }
        if (damage == Weapon.DamageType.Magical)
        {
            //Debug.Log("Difesa Utilizzata Res " + "Attaco " + attk.atk + " Difesa " + def.res);
            return attk.atk - def.res;
        }
        return 0;
    }
}

