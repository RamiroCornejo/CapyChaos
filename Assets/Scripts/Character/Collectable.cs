using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : TriggerReceptor
{
    public ParticleSystem collectParticle = null;
    [SerializeField] Transform parent = null;
    public GameObject prefab;

    private void Start()
    {
        ParticlesManager.Instance.GetParticlePool(collectParticle.name, collectParticle);
        Main.instance.AddCollectable();

        Instantiate(prefab, parent);
    }

    protected override void OnExecute(Character c)
    {
        ParticlesManager.Instance.PlayParticle(collectParticle.name, transform.position);
        Main.instance.CollectItem();
        SoundFX.Play_capi_recolect();
        Destroy(gameObject);
    }

    protected override void OnExit(Character c)
    {
    }
}
