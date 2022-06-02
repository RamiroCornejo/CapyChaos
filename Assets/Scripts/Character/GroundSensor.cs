using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] LayerMask floorMask = 1 << 0;
    [SerializeField] float rayDistance = 0.5f;
    [SerializeField] float distanceToDead = 10f;
    [SerializeField] float capibaraWeigth = 0.2f;
    public bool isGrounded = true;
    public float currentY;

    public Action DistanceComplete = delegate { };
    public Action OnGround = delegate { };
    public Action NotOnGround = delegate { };

    private void Awake()
    {
        currentY = transform.position.y;
    }
    public void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, rayDistance, floorMask, QueryTriggerInteraction.Ignore))
        {
            CheckHeigth(hit);
        }
        else if (Physics.Raycast(transform.position + transform.forward * capibaraWeigth, -transform.up, out hit, rayDistance, floorMask, QueryTriggerInteraction.Ignore))
        {
            CheckHeigth(hit);
        }
        else if (Physics.Raycast(transform.position - transform.forward * capibaraWeigth, -transform.up, out hit, rayDistance, floorMask, QueryTriggerInteraction.Ignore))
        {
            CheckHeigth(hit);
        }
        else
        {
            if (isGrounded) NotOnGround();
            isGrounded = false;
        }
    }

    void CheckHeigth(RaycastHit hit)
    {
        if (!isGrounded)
        {
            if (currentY - hit.point.y >= distanceToDead) DistanceComplete();
            //Debug.Log(currentY - hit.point.y);
            OnGround();
        }
        currentY = hit.point.y;
        isGrounded = true;
    }
}
