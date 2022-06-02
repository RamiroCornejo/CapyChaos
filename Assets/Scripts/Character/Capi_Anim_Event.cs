using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capi_Anim_Event : MonoBehaviour
{
    [SerializeField] GroundSensor GroundSensor;

    public void ANIM_EVENT_Footstep_A()
    {
        //habria que hacerle un sequencer para los sonidos de los pasos para que no repita siempre lo mismo
       if(GroundSensor.isGrounded) SoundFX.Play_capi_footsepA();
    }
    public void ANIM_EVENT_Footstep_B()
    {
        //habria que hacerle un sequencer para los sonidos de los pasos para que no repita siempre lo mismo
        if (GroundSensor.isGrounded) SoundFX.Play_capi_footsepB();
    }
}
