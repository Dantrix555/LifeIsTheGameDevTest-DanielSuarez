using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parabolic bullet controller
/// </summary>
public class ParabolicBullet : Bullet
{

    #region Bullet Inheritance

    public override void Shot(Transform shotOrigin)
    {
        gameObject.SetActive(true);
        IsInUse = true;
        transform.position = shotOrigin.position;
        bulletRigidbody.velocity = shotOrigin.transform.forward * bulletData.bulletSpeed;
        Invoke("DeactivateBullet", bulletData.spawnTime);
    }

    #endregion

}
