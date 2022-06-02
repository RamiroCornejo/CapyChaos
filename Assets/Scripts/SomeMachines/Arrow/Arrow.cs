using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arrow : MonoBehaviour
{
    public Collider sensor;
    public Collider platform;
    public ArrowSensor arrowSensor;

    Arrow myTail_Neghborhood;

    Action OnDestroyCallback;

    [SerializeField] GameObject myModel;
    [SerializeField] GameObject MyAnimGo;
    [SerializeField] Animator myAnim;

    public void SetMyTail(Arrow tail)
    {
        myTail_Neghborhood = tail;
    }

    private void Start()
    {
        StateOnAir();
        MyAnimGo.SetActive(false);
        myModel.SetActive(true);

    }

    public void Shoot(Action OnDestroy, Action OnStop, Action StopMovement)
    {
        MyAnimGo.SetActive(false);
        myModel.SetActive(true);

        sensor.transform.localPosition = Vector3.zero;
        platform.transform.localPosition = Vector3.zero;
        sensor.transform.localEulerAngles = Vector3.zero;
        platform.transform.localEulerAngles = Vector3.zero;
        OnDestroyCallback = OnDestroy;
        StateOnAir();
        arrowSensor.ConfigureSensor(value => { if (value) { StopMovement(); Destroy(); } else OnDestroy.Invoke(); }, () => { OnStop.Invoke(); StateOnWall(); }, () => { OnStop.Invoke(); ShutDownAll(); });
    }

    //este viene de Arrow destructible
    public void Destroy()
    {
        Debug.Log("De donde viene");

        Invoke("FinishDestroy", 1f);
        SoundFX.Play_arrow_hit_destroy();
        MyAnimGo.SetActive(true);
        myModel.SetActive(false);
        myAnim.Play("FlechaSeRompe");
        ShutDownAll();
        if (myTail_Neghborhood)
        {
            myTail_Neghborhood.Destroy();
            myTail_Neghborhood = null;
        }
    }

    void FinishDestroy()
    {
        OnDestroyCallback.Invoke();
    }

    void StateOnAir()
    {
        sensor.enabled = true;
        platform.enabled = false;
    }

    void StateOnWall()
    {
        Debug.Log("se activo");
        sensor.enabled = false;
        platform.enabled = true;
    }

    void ShutDownAll()
    {
        sensor.enabled = false;
        platform.enabled = false;
    }
}
