using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TicTac : MonoBehaviour
{
    public float speed = 2;
    bool anim = false;
    float timer = 0;
    bool isTic = false;

    [SerializeField] UnityEvent On_Tic;
    [SerializeField] UnityEvent On_Tac;

    public void Begin()
    {
        anim = true;
        timer = 0;
        isTic = true;
    }
    public void Stop()
    {
        anim = false;
        timer = 0;
        isTic = true;
        On_Tac.Invoke();
    }

    private void Update()
    {
        if (anim)
        {
            if (timer < 1)
            {
                timer = timer + speed * Time.deltaTime;
            }
            else
            {
                timer = 0;
                isTic = !isTic;
                if (isTic) Tic();
                else Tac();
            }
        }
    }

    void Tic() { SoundFX.Play_Tic(); On_Tic.Invoke(); }
    void Tac() { SoundFX.Play_Tac(); On_Tac.Invoke(); }
}
