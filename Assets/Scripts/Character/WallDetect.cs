using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallDetect : MonoBehaviour
{
    [SerializeField] LayerMask wallMask = 1 << 0;
    [SerializeField] float sensorDistance = 0.5f;

    [SerializeField] Transform model = null;
    [SerializeField] float cdTime = 0.3f;

    bool cooldown;
    float cd;

    public Action SensorCallback;

    public void CheckSensor()
    {
        if (cooldown)
        {
            cd += Time.deltaTime;
            if(cd >= cdTime)
            {
                cd = 0;
                cooldown = false;
            }
            return;
        }

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward,out hit, sensorDistance, wallMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform == model) return;
            SensorCallback?.Invoke();
            cooldown = true;
        }
        else if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.forward, out hit, sensorDistance, wallMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform == model) return;
            SensorCallback?.Invoke();
            cooldown = true;
        }
    }
}
