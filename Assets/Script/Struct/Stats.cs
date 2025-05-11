using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Statistiche condivise tra Hero e Weapon
public struct Stats
{
    public int atk;
    public int def;
    public int res;
    public int spd;
    public int crt;
    public int aim;
    public int eva;

    public Stats(int atk, int def, int res, int spd, int crt, int aim, int eva) //THE Cotruttore
    {
        this.atk = atk;
        this.def = def;
        this.res = res;
        this.spd = spd;
        this.crt = crt;
        this.aim = aim;
        this.eva = eva;
    }

    public static Stats Sum(Stats a, Stats b) // Prendiamo Tutte le statistiche di uno (Hero) le sommiamo con le statistiche di un altro (Weapon) e otteniamo TotStatistiche  
    {
        int atk = a.atk + b.atk;
        int def = a.def + b.def;
        int res = a.res + b.res;
        int spd = a.spd + b.spd;
        int crt = a.crt + b.crt;
        int aim = a.aim + b.aim;
        int eva = a.eva + b.eva;

        return new Stats(atk, def, res, spd, crt, aim, eva);   // Se voglio usare qualcosa in particolare usero TotStatistiche.atk
    }
}
