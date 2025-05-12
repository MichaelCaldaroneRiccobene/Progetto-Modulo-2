using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    // Heroi per la battaglia
    public Hero heroA;
    public Hero heroB;

    private Hero hero1; // Determina chi attacca per primo
    private Hero hero2; // Determina chi attacca per secondo

    private void Start()
    {
        // Si sommano le statistiche dell'Hero con le statistiche dell wepon
        Stats totStatsHeroA = CaculateStatsTot(heroA);
        Stats totStatsHeroB = CaculateStatsTot(heroB);

        //Decide chi attacca per primo
        AttackFirst(totStatsHeroA, totStatsHeroB, heroA, heroB);

        //Controllo Se uno dei due Hero hanno 0 Hp all'inizio
        if (!heroA.IsAlive() || !heroB.IsAlive())
        {
            Debug.Log("Uno o tutti gli Hero hanno 0 HP"); enabled = false;
        }
    }

    private void Update()
    {
        // Avviene L'attacco in modo automatico
        TurnForGoAttack(hero1, hero2);
    }

    //Prende le statistiche Totali degli Hero e determina chi deve attaccare primo Assegnandoli rispettivamente hero1 e hero2
    void AttackFirst(Stats heroAStats, Stats heroBStats, Hero heroA, Hero heroB)
    {
        //Se heroA e superiore a heroB in spd Allora heroA sarà hero1 e heroB sarà hero2 o viceversa
        if    (heroAStats.spd >= heroBStats.spd) {hero1 = heroA; hero2 = heroB; }
        else  {hero1 = heroB; hero2 = heroA; }
    }

    // Prende le statistiche del Hero e le statistiche dell'Arma del Hero e le somma
    Stats CaculateStatsTot(Hero hero)
    {
        return Stats.Sum(hero.BaseStats, hero.Weapon.BonusStats);
    }

    // Assegna l'ordine per attacare 
    void TurnForGoAttack(Hero heroA, Hero heroB)
    {
        // Inizio Turno 
        GoAttack(heroA, heroB);

        // Secondo Turno
        GoAttack(heroB, heroA);
    }
    void GoAttack(Hero heroA, Hero heroB)
    {
        // Controlla che tutti e due siano vivi prima di fare il codice 
        if (heroA.IsAlive() && heroB.IsAlive())
        {
            Debug.Log("Attaccante " + heroA.Name + " Attacca con elemento " + heroA.Weapon.Element + " Difensore " + heroB.Name + " Debolezza " + heroB.Weakness);

            // Si sommano le statistiche dell'Hero con le statistiche dell wepon
            Stats totStatsHeroA = CaculateStatsTot(heroA);
            Stats totStatsHeroB = CaculateStatsTot(heroB);

            Debug.Log("Attk Base " + heroA.BaseStats.atk + " Tot Attack " + totStatsHeroA.atk);
            //Solo se colpice da il danno
            if (GameFormulas.HasHit(totStatsHeroA, totStatsHeroB))
            {
                //Calcola il danno base che L'attaccante dovra fare al Difensore
                int danno = (GameFormulas.CalculateDamage(heroA, heroB));

                //Vediamo se il difensore è Debole o restistente
                if (GameFormulas.HasElementAdvantage(heroA.Weapon.Element, heroB)) { Debug.Log("Weak"); }
                if (GameFormulas.HasElementDisadvantage(heroA.Weapon.Element, heroB)) { Debug.Log("Resisten"); }

                heroB.TakeDamage(danno);
                Debug.Log("Danno Attaccante " + danno + " Vita difensore " + heroB.Hp);
            }
            //Il difensore non ha più il danno quindi l'attaccante vince 
            if (!heroB.IsAlive())
            {
                Debug.Log(" Vincitore " + heroA.Name);
                enabled = false;
            }
        }
    }
}
