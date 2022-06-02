using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Bullet bullet_model;
    public Transform pointShoot;

    public float time_to_shoot = 2;

    float timer;
    public bool automatic = false;

    bool game_init = false;

    float timer_cd;
    public float cd_shoot = 1;
    bool in_cd = false;

    [SerializeField] private Animator anim;

    public bool FirstArrowOnClick;

    public void BeginShoot()
    {
        game_init = true;
        if (FirstArrowOnClick)
        {
            Shoot();
        }
        timer_cd = 0;
    }

    private void Start()
    {
        BulletManager.Instance.GetBulletPool(bullet_model.name, bullet_model, 5);
    }

    public void Shoot()
    {
        if (!game_init) return;

        if (!in_cd)
        {
            anim.Play("Shoot");
            BulletManager.Instance.ShootBullet(bullet_model.name, pointShoot.transform.position, pointShoot.transform.forward, null, this);
            in_cd = true;
            SoundFX.Play_shooter_shoot();
        }
        
    }

    public void ReturnToPool(Bullet bullet)
    {
        BulletManager.Instance.ReturnBulletToPool(bullet, bullet_model.name);
    }


    private void Update()
    {
        if (!game_init) return;

        if (in_cd)
        {
            if (timer_cd < cd_shoot)
            {
                timer_cd = timer_cd + 1 * Time.deltaTime;
            }
            else
            {
                timer_cd = 0;
                in_cd = false;
            }
        }

        if (!automatic) return;
        if (timer < time_to_shoot)
        {
            timer = timer + 1 * Time.deltaTime;
        }
        else
        {
            timer = 0;
            Shoot();
        }

        
    }
}
