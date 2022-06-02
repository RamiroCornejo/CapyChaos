using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestCorpse : MonoBehaviour
{
    [SerializeField] Rigidbody[] parts = new Rigidbody[0];
    Vector3[] partsInitPosition = new Vector3[0];
    Vector3[] partsInitRotation = new Vector3[0];
    [SerializeField] Transform basePart = null;
    [SerializeField] float timeToDissappear = 4f;
    [SerializeField] float pushForce = 6;
    [SerializeField] float torqueForce = 4;

    [SerializeField] CapiPhysicPart head;
    [SerializeField] CapiPhysicPart corpse;

    float timer;

    public void Initialize()
    {
        partsInitPosition = new Vector3[parts.Length];
        partsInitRotation = new Vector3[parts.Length];
        for (int i = 0; i < partsInitPosition.Length; i++)
        {
            partsInitPosition[i] = parts[i].transform.localPosition;
            partsInitRotation[i] = parts[i].transform.localEulerAngles;
            parts[i].isKinematic = true;
        }
    }

    public void Disarm()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].isKinematic = false;
            Vector3 aux;
            if (parts[i].transform != basePart) aux = parts[i].transform.position - basePart.position;
            else aux = Vector3.right;
            parts[i].AddForce(aux * pushForce, ForceMode.VelocityChange);
            parts[i].AddTorque(aux * torqueForce);
        }
    }

    Action OnEndEnvent = delegate { };
    public void Disarm(bool in_head, GameObject object_reference, Action OnEndEvent)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].isKinematic = false;
            Vector3 aux;
            if (parts[i].transform != basePart) aux = parts[i].transform.position - basePart.position;
            else aux = Vector3.right;
            parts[i].AddForce(aux * pushForce, ForceMode.VelocityChange);
            parts[i].AddTorque(aux * torqueForce);
        }

        this.OnEndEnvent = OnEndEvent;



        if (in_head) head.StickAndForce(pushForce, object_reference);
        else corpse.StickAndForce(pushForce, object_reference);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeToDissappear)
        {
            OnEndEnvent?.Invoke();
            Dissappear();
        }
    }

    void Dissappear()
    {
        timer = 0;
        for (int i = 0; i < partsInitPosition.Length; i++)
        {
            parts[i].transform.localPosition = partsInitPosition[i];
            parts[i].transform.localEulerAngles = partsInitRotation[i];
            parts[i].isKinematic = true;
        }
        if(Main.instance != null)
            Main.instance.ReturnCorpse(this);
        else
            CreditsManager.instance.ReturnCorpse(this);
    }
}
