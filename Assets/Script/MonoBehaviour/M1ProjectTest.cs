using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    [Header("Condizioni")]// Decido le Condizioni Della Battaglia 
    public bool isPrestabilito = true;
    public bool isAutomatic = true;
    public bool isSlow = true;
    [Range(0.01f, 2f)]
    public float velocityBattle = 1f;

    //[Header("Armi")] // Creo le mie Armi
    //public Weapon weponA;
    //public Weapon weponB;

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

            heroA = new Hero("Giggino", 100, new Stats(20, 13, 4, 20, 15, 27, 17), Element.Fire, Element.Ice, weponA);
            heroB = new Hero("Jim", 100, new Stats(20, 13, 4, 18, 13, 27, 17), Element.Lightning, Element.Fire, weponB);
        }

        // Si sommano le statistiche dell'Hero con le statistiche dell wepon
        Stats statsCombo1 = Stats.Sum(heroA.baseStats, heroA.weapon.bonusStats); 
        Stats statsCombo2 = Stats.Sum(heroB.baseStats, heroB.weapon.bonusStats);

        //Decide chi attacca per primo
        AttackFirst(statsCombo1, statsCombo2, heroA, heroB);

        //Combattimento Lento
        if (isSlow && isAutomatic) StartCoroutine(SlowBattle(hero1, hero2));
    }

    private void Update()
    {
        // Avviene L'attacco in modo automatico o manuale
        if (!isSlow) if (isAutomatic) { Attacco(hero1, hero2); } 
        else if (Input.GetKeyDown(KeyCode.Space)) { Attacco(hero1, hero2); }
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
            Debug.Log("Attaccante " + a.name + " Attacca con elemento " + a.weapon.element + " Difensore " + b.name + " Debolezza " + b.weakness);

            Stats statsCombo1 = Stats.Sum(a.baseStats, a.weapon.bonusStats);
            Stats statsCombo2 = Stats.Sum(b.baseStats, b.weapon.bonusStats);

            //Solo se colpice da il danno
            if (GameFormulas.HasHit(statsCombo1, statsCombo2))
            {
                //Calcola il danno base che L'attaccante dovra fare al Difensore
                int danno = (GameFormulas.CalculateDamage(a, b));

                Debug.Log("Danno Ricevuto Base " + danno + " Salute Difensore " + b.hp);

                //Vediamo se il difensore è Debole o restistente
                if (GameFormulas.HasElementAdvantage(a.weapon.element, b)) { Debug.Log("Weak"); }
                if (GameFormulas.HasElementDisadvantage(a.weapon.element, b)) { Debug.Log("Resisten"); }

                b.TakeDamage(danno);
            }
            //Il difensore non ha più il danno quindi l'attaccante vince 
            if (!b.IsAlive())
            {
                Debug.Log(" Vincitore " + a.name + " Vita Vincitore " + a.hp);
                this.enabled = false;
            }
        }
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

    IEnumerator SlowBattle(Hero a, Hero b)
    {
        // Aspetta un secondo prima di fare una determinata azione
        yield return new WaitForSeconds(velocityBattle); 
        // Inizio Attacco 
        GoAttack(a, b);
        yield return new WaitForSeconds(velocityBattle);
        // Secondo Attacco
        GoAttack(b, a);

        //Richiama se stesso in un ciclo infinito di lotta finche questo scritp non viene disabilitato 
        StartCoroutine(SlowBattle(a, b)); 
    }
}
