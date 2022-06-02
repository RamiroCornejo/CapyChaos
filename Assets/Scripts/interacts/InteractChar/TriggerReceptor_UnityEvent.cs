using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerReceptor_UnityEvent : TriggerReceptor
{
    public UnityEvent EV_OnExecute;
    public UnityEvent EV_OnExit;

    protected override void OnExecute(Character c)
    {
        EV_OnExecute.Invoke();
    }

    protected override void OnExit(Character c)
    {
        EV_OnExit.Invoke();
    }
}
