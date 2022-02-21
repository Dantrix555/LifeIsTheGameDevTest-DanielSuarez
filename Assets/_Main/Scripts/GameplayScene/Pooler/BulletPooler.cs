using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet pooler singleton that control main bullets flow and instantiation
/// </summary>
public class BulletPooler : BASESingleton<BulletPooler>
{

    protected BulletPooler() {}

    #region Fields and properties

    [Header("Bullet references")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private BulletsData bulletsData;
    [SerializeField]
    private List<GameObject> poolBulletGameObjects;

    private List<Bullet> bulletPool;
    private BulletData cachedBulletData;

    public BulletsData BulletsData => bulletsData;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set a new bullet type and update all bullets in the pool
    /// </summary>
    /// <param name="bulletType">Bullet type to update bullets data</param>
    public void UpdateBulletType(BulletType bulletType)
    {
        bulletPool = new List<Bullet>();

        for(int i = 0; i < poolBulletGameObjects.Count; i++)
        {
            Bullet bullet = poolBulletGameObjects[i].GetComponent<Bullet>();

            if (bullet != null)
                bullet.DestroyBulletReference();

            cachedBulletData = bulletsData.bulletsData.Find(bulletData => bulletType == bulletData.bulletType);

            switch (bulletType)
            {
                case BulletType.Parabolic:
                    ParabolicBullet parabolicBullet = poolBulletGameObjects[i].AddComponent<ParabolicBullet>();
                    parabolicBullet.SetBulletData(cachedBulletData);
                    bulletPool.Add(parabolicBullet);
                    break;

                case BulletType.Traction:
                    TractionBullet tractionBullet = poolBulletGameObjects[i].AddComponent<TractionBullet>();
                    tractionBullet.SetBulletData(cachedBulletData);
                    bulletPool.Add(tractionBullet);
                    break;

                case BulletType.Bouncy:
                    BouncyBullet bouncyBullet = poolBulletGameObjects[i].AddComponent<BouncyBullet>();
                    bouncyBullet.SetBulletData(cachedBulletData);
                    bulletPool.Add(bouncyBullet);
                    break;
            }
        }
    }

    /// <summary>
    /// Get a free bullet to shot, if not create a new bullet and expand the pool capacity
    /// </summary>
    /// <typeparam name="T">Generic type that inherits from abstract bullet class</typeparam>
    /// <returns>Free bullet reference</returns>
    public T GetBullet<T>() where T : Bullet
    {
        Bullet returnBullet = bulletPool.FirstOrDefault(bullet => !bullet.IsInUse);

        if (returnBullet == null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform);
            returnBullet = newBullet.GetComponent<T>() == null ? newBullet.AddComponent<T>() : newBullet.GetComponent<T>();
            returnBullet.SetBulletData(cachedBulletData);
            poolBulletGameObjects.Add(newBullet);
            bulletPool.Add(returnBullet);
            newBullet.SetActive(false);
        }

        return returnBullet as T;
    }

    #endregion
}
