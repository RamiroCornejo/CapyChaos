using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody firstStaticObject;
    [SerializeField] Slabon[] slabs;
    public AttachedObject attachedObject;

    private void Start()
    {
        slabs = GetComponentsInChildren<Slabon>();

        firstStaticObject.useGravity = false;
        firstStaticObject.isKinematic = true;

        for (int i = 0; i < slabs.Length; i++)
        {
            slabs[i].SetRope(this);
            slabs[i].Initialize();

            if (i > 0)
            {
                slabs[i].CharJoint.connectedBody = slabs[i - 1].MyRig;
            }
            else
            {
                slabs[i].CharJoint.connectedBody = firstStaticObject;
            }
        }
    }

    public void SlabCutted(Slabon slab)
    {
        attachedObject.Detach();

        if(slab.CharJoint) slab.CharJoint.connectedBody.WakeUp();

        slabs[slabs.Length-1].MyRig.WakeUp();

        for (int i = 0; i < slabs.Length; i++)
        {
            if (/*disconect && */slabs[i] != attachedObject.gameObject.GetComponent<Slabon>())
            {
                Destroy(slabs[i].gameObject);
                //slabs[i].MyRig.detectCollisions = false;
                slabs[i].MyRig.mass = 0;
                //slabs[i].MyRig.WakeUp();
            }
        }
    }
}
