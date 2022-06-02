using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback_SimpleButton : TriggerReceptor
{
    [SerializeField] ParticleSystem clickparticle = null;
    public Renderer render_button;
    public Transform pressed_pos;
    public Transform released_pos;
    public Transform button_element;
    public Animator myAnim;

    private void Start()
    {
        ParticlesManager.Instance.GetParticlePool(clickparticle.name, clickparticle);
    }

    protected override void OnExecute(Character c)
    {
        ParticlesManager.Instance.PlayParticle(clickparticle.name, transform.position);
        SoundFX.Play_capi_button_touch_begin();
        myAnim.Play("Push");

        button_element.transform.position = pressed_pos.transform.position;
        button_element.transform.localScale = pressed_pos.transform.localScale;
        render_button.material.SetFloat("_InteractSwitch", 0);

    }

    public void OnExecute()
    {
        ParticlesManager.Instance.PlayParticle(clickparticle.name, transform.position);
        SoundFX.Play_capi_button_touch_begin();
        myAnim.Play("Push");

        button_element.transform.position = pressed_pos.transform.position;
        button_element.transform.localScale = pressed_pos.transform.localScale;
        render_button.material.SetFloat("_InteractSwitch", 0);

    }

    protected override void OnExit(Character c)
    {
        SoundFX.Play_capi_button_touch_end();
        button_element.transform.position = released_pos.transform.position;
        button_element.transform.localScale = released_pos.transform.localScale;
        render_button.material.SetFloat("_InteractSwitch", 1);
    }
}
