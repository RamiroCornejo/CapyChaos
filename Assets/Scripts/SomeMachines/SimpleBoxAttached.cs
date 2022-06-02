using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoxAttached : AttachedObject
{
    [SerializeField] UserInteractable interactable;

    protected override void OnDetach()
    {
        //interactable.CancelLimits();
    }
}
