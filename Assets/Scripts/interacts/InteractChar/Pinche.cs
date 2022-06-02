using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinche : TriggerReceptor
{
    public Transform deathLimit;

    [Header("false para poner los pinches acostados")]
    public bool UseDeathLimit = true;

    protected override void OnExecute(Character c)
    {
        if (UseDeathLimit)
        {
            //si usa limites hago checkeo

            var charpos = c.transform.position;
            if (charpos.y > deathLimit.transform.position.y)
            {
                c.Kill();
            }
        }
        else 
        {
            //sino los usa, mato directamente al bicho
            //sirve para acostar los pinches, ponerlos en las paredes
            c.Kill();
        }
    }

    public void On()
    {

    }
    public void Off()
    {

    }

    protected override void OnExit(Character c)
    {

    }
}
