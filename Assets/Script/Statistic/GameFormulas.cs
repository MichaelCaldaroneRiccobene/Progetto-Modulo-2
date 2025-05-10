using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFormulas
{

    public static bool HasElementAdvantage(Element attkElement, Hero defender) // Prende Elemento Spada, e debolezza Hero
    {
        if (attkElement == defender.weakness)
        {
            return true;
        }
        else { return false; }
    }

    public static bool HasElementDisadvantage(Element attkElement, Hero defender)// Prende Elemento Spada, e resistenza Hero
    {
        if (attkElement == defender.resistance)
        {
            return true;
        }
        else { return false; }
    }

    public static float EvaluateElementModifier(Element attkElement, Hero defender)//Guarda se bisogna moltiplicare o dimezzare il danno
    {
        if (HasElementAdvantage(attkElement, defender))     return 1.5f;
        if (HasElementDisadvantage(attkElement, defender))  return 0.5f;
                                                            return 1;
    }

    public static bool HasHit(Stats attacker, Stats defender)//Controlla se si colpisce l'altro Hero 
    {
        float hitChance = attacker.aim - defender.eva;

        float randomaChance = UnityEngine.Random.Range(0, 100);

        if (hitChance > randomaChance) // Es HitChance e 60 > a Es randomChache 55 allora si colpisce se no niente 
        {
            return true;
        }
        else
        {
            Debug.Log("MISS");
            return false;
        }
    }

    public static bool IsCrit(int criticValue)// Controlla se si fa un critico 
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

    public static int CalculateDamage(Hero attacker, Hero defender) // Si calcola il Danno
    {
        Stats newStatsAttacker = Stats.Sum(attacker.baseStats, attacker.wepon.bonusStats);//Statistiche degli Hero sommati alle Weapon
        Stats newStatsDefender = Stats.Sum(defender.baseStats, defender.wepon.bonusStats);

        float risultatoAttaco = AttMenoDif(attacker.wepon.damageType, newStatsAttacker, newStatsDefender); // Che danno si applica e come ci si difende

        float dannoElemet = EvaluateElementModifier(attacker.wepon.element, defender);//Il danno viene radoppiato o dimezzato o rimane normale 
        risultatoAttaco *= dannoElemet;

        bool isCrit = IsCrit(newStatsAttacker.crt);// vediamo se è un critico 

        if (isCrit) { risultatoAttaco *= 2; } // se si si radoppia 
        if (risultatoAttaco < 0) { return 0; } //se è minore di 0 si mette 0

        int risulAttck = Mathf.FloorToInt(risultatoAttaco);
        return risulAttck; //Si riporta il risultato dell'Attacco
    }

    static float AttMenoDif(Weapon.DamageType damage, Stats attk, Stats def) // Funzione che vede come applicare il danno guardando come attacca l'attacante e come deve rispondere il difensore
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

