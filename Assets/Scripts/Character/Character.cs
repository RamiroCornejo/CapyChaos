using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField, Range(-1, 1)] float initDir = 1;
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] WallDetect wallSensor = null;
    [SerializeField] GroundSensor groundSensor = null;
    [SerializeField] ParticleSystem deathPart;

    public bool alreadyAgregate = false;
    float timer;
    Vector3 deltaPos;

    public enum platform_state { undef, up, down }
    [System.NonSerialized] public platform_state state_platform;


    private void Awake()
    {
        transform.forward = new Vector3(transform.forward.x * initDir, transform.forward.y, transform.forward.z);
        wallSensor.SensorCallback += Rotate;
        groundSensor.DistanceComplete += Kill;
        
    }
    private void Start()
    {
        if (!alreadyAgregate) { Main.instance.AddCharacter(this); alreadyAgregate = true; }
        ParticlesManager.Instance.GetParticlePool(deathPart.name, deathPart);

        SoundFX.Play_capi_spawn();
    }

    void Rotate()
    {
        transform.forward = new Vector3(-transform.forward.x, transform.forward.y, transform.forward.z);
        timer = 0;

    }

    private void Update()
    {
        wallSensor.CheckSensor();
        groundSensor.CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);

        if(Vector3.Distance(deltaPos, transform.position) <= 0.1f)
        {
            timer += Time.deltaTime;
            if(timer >= 0.2f)
            {
                Rotate();
                timer = 0;
            }
        }
        else
        {
            deltaPos = transform.position;
            timer = 0;
        }
    }

    public void EnterToWinZone()
    {
        Main.instance.CharacterWin(this);
        Destroy(this.gameObject);
        SoundFX.Play_capi_sucessfull();
    }

    public void Kill()
    {
        //feedback gore
        //pool si hay tiempo
        Main.instance.ActiveCorpse(transform);
        Main.instance.RemoveCharacter(this);
        ParticlesManager.Instance.PlayParticle(deathPart.name, this.transform.position);
        SpawnManager.instanciate.UnSubscribeCharacter(this);
        Destroy(this.gameObject);
        SoundFX.Play_Blood_Splash();
        SoundFX.Play_capi_death();
        CameraShake.Shake();
    }
    public void Kill(bool in_head, GameObject object_reference, Action OnEndEvent)
    {
        //feedback gore
        //pool si hay tiempo
        Main.instance.ActiveCorpse(transform, in_head, object_reference, OnEndEvent);
        Main.instance.RemoveCharacter(this);
        ParticlesManager.Instance.PlayParticle(deathPart.name, this.transform.position);
        Destroy(this.gameObject);
        SpawnManager.instanciate.UnSubscribeCharacter(this);
        SoundFX.Play_Blood_Splash();
        SoundFX.Play_capi_death();
    }

}
