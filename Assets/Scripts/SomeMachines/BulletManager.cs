using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    private Dictionary<string, BulletPool> bulletRegistry = new Dictionary<string, BulletPool>();

    private void Awake() { if (Instance == null) Instance = this; }

    public Bullet ShootBullet(string bulletName, Vector3 spawnPos, Vector3 dir, Transform trackingTransform = null, Shooter shooter = null)
    {
        if (bulletRegistry.ContainsKey(bulletName))
        {
            var element = bulletRegistry[bulletName];

            Bullet bullet = element.Get();
            bullet.transform.position = spawnPos;
            bullet.transform.forward = dir;
            if (trackingTransform != null) bullet.transform.SetParent(trackingTransform);
            bullet.Play(shooter);
            return bullet;
        }
        else
        {
            Debug.LogWarning("No tenes ese bullet en el pool");
            return null;
        }
    }


    //public void StopBullet(string particleName, ParticleSystem particle)
    //{
    //    if (bulletRegistry.ContainsKey(particleName))
    //    {
    //        var particlePool = bulletRegistry[particleName];
    //        particlePool.ReturnParticle(particle);
    //        if (particles.ContainsKey(particle))
    //        {
    //            ParticlesUpdater -= particles[particle];

    //            particles.Remove(particle);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No tenes ese sonido en en pool");
    //    }
    //}

    //public void StopAllParticles(string particleName)
    //{
    //    if (bulletRegistry.ContainsKey(particleName))
    //    {
    //        bulletRegistry[particleName].StopAllParticles();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No tenes ese sonido en en pool");
    //    }
    //}

    public BulletPool GetBulletPool(string bulletname, Bullet bullet = null, int prewarmAmount = 2)
    {
        if (bulletRegistry.ContainsKey(bulletname)) return bulletRegistry[bulletname];
        else if (bullet != null) return CreateNewBulletPool(bullet, bulletname, prewarmAmount);
        else return null;
    }

    public void DeleteParticlePool(string particleName)
    {
        if (bulletRegistry.ContainsKey(particleName))
        {
            Destroy(bulletRegistry[particleName].gameObject);
            bulletRegistry.Remove(particleName);
        }
    }

    #region Internal
    private BulletPool CreateNewBulletPool(Bullet bullet, string bulletName, int prewarmAmount = 2)
    {
        var bulletpool = new GameObject($"{bulletName} BulletPool").AddComponent<BulletPool>();
        bulletpool.transform.SetParent(transform);
        bulletpool.ConfigureBullet(bullet);
        bulletpool.Initialize(prewarmAmount);
        bulletRegistry.Add(bulletName, bulletpool);
        return bulletpool;
    }

    public void ReturnBulletToPool(Bullet bullet, string bulletName)
    {
        if (bullet.gameObject.activeSelf)
        {
            bulletRegistry[bulletName].ReturnBullet(bullet);
        }
    }

    //float ObtainDuration(ParticleSystem pS)
    //{
    //    float higher = pS.main.duration;

    //    var childrens = pS.GetComponentsInChildren<ParticleSystem>();

    //    for (int i = 0; i < childrens.Length; i++)
    //    {
    //        if (higher < childrens[i].main.duration) higher = childrens[i].main.duration;
    //    }

    //    return higher;
    //}
    #endregion
}
