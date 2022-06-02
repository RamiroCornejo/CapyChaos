using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevelopTools;

public class BulletPool : SingleObjectPool<Bullet>
{
    [SerializeField] private Bullet bulletModel;

    public void ConfigureBullet(Bullet bullet)
    {
        bulletModel = bullet;
    }

    protected override void AddObject(int amount = 5)
    {
        var bullet = Instantiate(bulletModel);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(transform);
        bullet.transform.position = transform.position;
        objects.Enqueue(bullet);
    }

    public void ReturnBullet(Bullet bullet)
    {
        if (bullet == null) return;
        bullet.transform.SetParent(transform);
        bullet.Stop();
        ReturnToPool(bullet);
    }

    #region Pause & Resume
    public void PauseAllBullets()
    {
        for (int i = 0; i < currentlyUsingObj.Count; i++)
        {
            if (currentlyUsingObj[i] == null)
            {
                currentlyUsingObj.RemoveAt(i);
                i -= 1;
                continue;
            }

            currentlyUsingObj[i].Pause();
        }
    }
    public void ResumeBullets()
    {
        for (int i = 0; i < currentlyUsingObj.Count; i++)
        {
            if (currentlyUsingObj[i] == null)
            {
                currentlyUsingObj.RemoveAt(i);
                i -= 1;
                continue;
            }
            currentlyUsingObj[i].Resume();
        }
    }
    public void StopAllBullets()
    {
        for (int i = currentlyUsingObj.Count - 1; i >= 0; i--)
        {
            currentlyUsingObj[i].Stop();
            ReturnToPool(currentlyUsingObj[i]);
        }
    }
    #endregion
}
