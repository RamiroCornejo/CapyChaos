using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySomething : TriggerReceptor
{
    public FreeRockDrageable myRock;

    protected override void OnExecute(Character c)
    {
        myRock.AddCapi(c);
    }

    protected override void OnExit(Character c)
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Arrow_Destructible destructible = other.GetComponent<Arrow_Destructible>();

        if (destructible)
        {
            destructible.DestroyArrow();
        }
    }

}
