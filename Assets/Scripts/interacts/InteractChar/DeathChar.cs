using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathChar : TriggerReceptor
{

    protected override void OnExecute(Character c)
    {
        c.Kill();
    }

    protected override void OnExit(Character c)
    {
        
    }

}
