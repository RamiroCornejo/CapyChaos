using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool anim;
    [SerializeField] float speed = 5;

    bool startCD;
    float timer;
    public float time_to_destroy = 5;

    float time_to_before_feedback_destroy = 2f;
    bool oneshotanim;

    Shooter shooter;

    public Arrow myArrow;
    public Animator myAnim;


    public void Play(Shooter shooter)
    {
        anim = true;
        myArrow.Shoot(DestroyBullet, PauseAndTimeToDestroy, StopMovement);
        this.shooter = shooter;
        oneshotanim = false;
        timer = 0;
    }


    private void Update()
    {
        if (anim)
        {
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }
        if (startCD)
        {
            if (timer < time_to_destroy)
            {
                timer = timer + 1 * Time.deltaTime;

                if (timer > time_to_destroy - time_to_before_feedback_destroy && !oneshotanim)
                {
                    myAnim.Play("AnimTitila");
                    oneshotanim = true;
                }
            }
            else
            {
                timer = 0;
                startCD = false;
                myArrow.Destroy();
                oneshotanim = false;
            }
        }
    }

    public void StopMovement()
    {
        anim = false;
    }

    public void DestroyBullet()
    {
        anim = false;
        shooter.ReturnToPool(this);
    }
    public void PauseAndTimeToDestroy()
    {
        anim = false;
        startCD = true;
    }

    
    public void Stop() => anim = false;
    public void Pause() => anim = false;
    public void Resume() => anim = true;
}
