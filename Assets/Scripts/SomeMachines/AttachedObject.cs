using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttachedObject : MonoBehaviour
{
    [SerializeField] Rigidbody myRig;

    public void Detach()
    {
        OnDetach();
        myRig.isKinematic = false;
    }

    private void Start()
    {
        //myRig.isKinematic = true;
    }

    protected abstract void OnDetach();
}
