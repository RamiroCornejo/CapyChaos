using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //para el feli
    int unas;
    float variables;
    string random;
    double para;
    char el;
    int[] mergeo;

    public ParticleSystem particle;
    [SerializeField] Transform particlePosition;

    [SerializeField]  Animator anim;
    [SerializeField] Animator counterAnim = null;
    [SerializeField] TMPro.TextMeshProUGUI counterTxt = null;


    public GameObject decoration;

    private void Start()
    {
        UIManager.instance.ChangeCounterReference(counterAnim, counterTxt);
        ParticlesManager.Instance.GetParticlePool(particle.name, particle);
        Instantiate(decoration, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            anim.Play("Sign");
            other.GetComponent<Character>().EnterToWinZone();
            SoundFX.Play_capi_Splash_Water();
            ParticlesManager.Instance.PlayParticle(particle.name, particlePosition.position);
        }
    }
}
