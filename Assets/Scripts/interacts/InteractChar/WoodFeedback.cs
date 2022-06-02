using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFeedback : MonoBehaviour
{
    public ParticleSystem particle;

    public void StartAnimWood()
    {
        SoundFX.Play_begin_drag_wood();
        //particle.Play();
    }
    public void StopAnimWood()
    {
        SoundFX.Play_end_drag_wood();
        particle.Play();
    }
}
