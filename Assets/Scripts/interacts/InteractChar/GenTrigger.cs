using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Tools.EventClasses;

[RequireComponent(typeof(Collider))]
public class GenTrigger : MonoBehaviour
{
    TriggerReceptor[] receptors;
    Collider myCol;

    private void Start()
    {
        receptors = GetComponents<TriggerReceptor>();
        var c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var c = other.GetComponent<Character>();
        if (c != null)
        {
            foreach (var r in receptors) r.Execute(c);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var c = other.GetComponent<Character>();
        if (c != null)
        {
            foreach (var r in receptors) r.Exit(c);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var c = other.GetComponent<Character>();
        if (c != null)
        {
            foreach (var r in receptors) r.Stay(c);
        }
    }
}
