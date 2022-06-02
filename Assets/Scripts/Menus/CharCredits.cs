using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCredits : MonoBehaviour
{
    [SerializeField, Range(-1, 1)] float initDir = 1;
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] WallDetect wallSensor = null;
    [SerializeField] GroundSensor groundSensor = null;
    [SerializeField] ParticleSystem deathPart;
    [SerializeField] TMPro.TextMeshProUGUI text;

    public bool alreadyAgregate = false;


    private void Awake()
    {
        wallSensor.SensorCallback += Rotate;
        groundSensor.DistanceComplete += Kill;

    }
    private void Start()
    {
        ParticlesManager.Instance.GetParticlePool(deathPart.name, deathPart);
    }

    void Rotate()
    {
        transform.forward = new Vector3(-transform.forward.x, transform.forward.y, transform.forward.z);
    }

    private void Update()
    {
        wallSensor.CheckSensor();
        groundSensor.CheckGround();
    }

    public void Spawn()
    {
        text.text = CreditsManager.instance.GetName();
        SoundFX.Play_capi_spawn();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector3(transform.forward.x * speed, rb.velocity.y, transform.forward.z * speed);
    }

    public void Kill()
    {
        CreditsManager.instance.ActiveCorpse(transform);
        ParticlesManager.Instance.PlayParticle(deathPart.name, this.transform.position);
        SoundFX.Play_Blood_Splash();
        SoundFX.Play_capi_death();
        transform.forward = new Vector3(transform.forward.x * initDir, transform.forward.y, transform.forward.z);
        gameObject.SetActive(false);
    }
}
