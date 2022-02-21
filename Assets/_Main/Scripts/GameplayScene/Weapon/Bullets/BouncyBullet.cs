using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bouncy bullet controller
/// </summary>
public class BouncyBullet : Bullet
{
    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == bulletData.InteractionLayer)
        {
            bulletRigidbody.velocity *= 0.75f;
            bulletRigidbody.velocity += other.transform.up * (bulletData.bulletSpeed / 2);
            VFXExplosionPooler.Instance.SetExplosionVFX(transform);
        }
        else
            DeactivateBullet();
    }

    #endregion

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
