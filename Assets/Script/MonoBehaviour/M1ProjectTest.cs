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
    //public Wepon weponA;
    //public Wepon weponB;

    [Header("Eroi")] // Creo i miei Hero
    public Hero heroA;
    public Hero heroB;

    private Hero hero1; // Determina chi attacca per primo
    private Hero hero2; // Determina chi attacca per secondo

    private void Start()
    {
        if (isPrestabilito) //Si mettono già i settaggi per le Wepons e Heros, si puo anche non fare
        {
            Wepon weponA = new Wepon("Thunger", Wepon.DamageType.Physical, Element.Fire, new Stats(1, 5, 2, 60, 1, 4, 6));
            Wepon weponB = new Wepon("Loca", Wepon.DamageType.Magical, Element.Lightning, new Stats(1, 5, 2, 60, 1, 4, 6));

            heroA = new Hero("Giggino", 100, new Stats(25, 5, 2, 60, 15, 4, 6), Element.Fire, Element.Ice, weponA);
            heroB = new Hero("Jim", 100, new Stats(25, 5, 2, 60, 15, 4, 6), Element.Lightning, Element.Fire, weponB);
        }

        Stats statsCombo1 = Stats.Sum(heroA.baseStats, heroA.wepon.bonusStats); // Si sommano le statistiche dell'Hero con le statistiche dell wepon
        Stats statsCombo2 = Stats.Sum(heroB.baseStats, heroB.wepon.bonusStats);

        AttackFirst(statsCombo1, statsCombo2, heroA, heroB);//Decide chi attacca per primo

        if(isSlow && isAutomatic) StartCoroutine(SlowBattle(hero1, hero2));//Combattimento Per nonneti 
    }

    private void Update()
    {
        if (!isSlow) if (isAutomatic) { Attacco(hero1, hero2); } else if (Input.GetKeyDown(KeyCode.Space)) { Attacco(hero1, hero2); } // Avviene L'attacco in modo automatico o manuale
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
        if (a.IsAlive() && b.IsAlive()) // Controlla che tutti e due siano vivi prima di fare il codice 
        {
            Debug.Log("Attaccante " + a.name + " Attacca con elemento " + a.wepon.element + " Difensore " + b.name + " Debolezza " + b.weakness);

            Stats statsCombo1 = Stats.Sum(a.baseStats, a.wepon.bonusStats);
            Stats statsCombo2 = Stats.Sum(b.baseStats, b.wepon.bonusStats);

            //Solo se colpice da il danno
            if (GameFormulas.HasHit(statsCombo1, statsCombo2))
            {
                int danno = (GameFormulas.CalculateDamage(a, b));//Calcola il danno base che L'attaccante dovra fare al Difensore

                Debug.Log("Danno Ricevuto Base " + danno);

                //Vediamo se il difensore è Debole o restistente
                if (GameFormulas.HasElementAdvantage(a.wepon.element, b)) { Debug.Log("Weak"); }
                if (GameFormulas.HasElementDisDadvantage(a.wepon.element, b)) {Debug.Log("Resisten"); }

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
    void AttackFirst(Stats a, Stats b,Hero z,Hero y)
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
        yield return new WaitForSeconds(velocityBattle); // Aspetta un secondo prima di fare una determinata azione
        // Inizio Attacco 
        GoAttack(a, b);
        yield return new WaitForSeconds(velocityBattle);
        // Secondo Attacco
        GoAttack(b, a);

        StartCoroutine(SlowBattle(a, b)); //Richiama se stesso in un ciclo infinito di lotta finche questo scritp non viene disabilitato 
    }
}
