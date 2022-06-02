using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAMPLE_TriggerReceptor_DEBUGLOG : TriggerReceptor
{
    protected override void OnExecute(Character c)
    {
        Debug.Log("ENTER.. Char: " + c.name);
    }

    protected override void OnExit(Character c)
    {
        Debug.Log("EXIT.. Char: " + c.name);
    }
}
