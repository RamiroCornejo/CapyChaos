using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapiPhysicPart : MonoBehaviour
{
    [SerializeField] Rigidbody myRig;
    Vector3 SoftUpDir;

    private void Start()
    {
        SoftUpDir = Vector3.up * 0.5f;
    }

    public void StickAndForce(float force, GameObject stickedObject)
    {
        stickedObject.transform.SetParent(this.transform);

        myRig.mass = myRig.mass * 2;
        myRig.AddForce(stickedObject.transform.forward + SoftUpDir * force * 5, ForceMode.VelocityChange);

    }
}
