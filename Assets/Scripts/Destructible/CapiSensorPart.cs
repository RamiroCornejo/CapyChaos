using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CapiSensorPart : MonoBehaviour
{
    public Character capibara;
    public bool isHead;

    public void Hit(GameObject stickeable, Action OnEndEvent)
    {
        capibara.Kill(isHead, stickeable, OnEndEvent);
    }

}
