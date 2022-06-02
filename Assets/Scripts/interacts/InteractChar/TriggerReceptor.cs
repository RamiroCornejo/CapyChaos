using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerReceptor : MonoBehaviour
{
    public void Execute(Character character)
    {
        OnExecute(character);
    }

    public void Exit(Character character)
    {
        OnExit(character);
    }
    public void Stay(Character character)
    {
        OnStay(character);
    }

    protected abstract void OnExecute(Character c);
    protected abstract void OnExit(Character c);
    protected virtual void OnStay(Character c) { }
}
