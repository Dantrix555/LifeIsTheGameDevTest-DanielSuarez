using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player weapon positioning and use
/// </summary>
public class PlayerWeapon : MonoBehaviour
{
    #region Fields and properties

    private Weapon actualPlayerWeapon;
    private bool canShot = true;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set new weapon to player and update new weapon visible transform
    /// </summary>
    /// <param name="newWeapon">New player weapon reference</param>
    public void SetNewWeaponToPlayer(Weapon newWeapon)
    {
        if (actualPlayerWeapon != null)
            actualPlayerWeapon.ResetWeaponLocation();

        actualPlayerWeapon = newWeapon;
        actualPlayerWeapon.SetWeaponNewLocation(transform);
        BulletPooler.Instance.UpdateBulletType(actualPlayerWeapon.BulletType);
    }

    /// <summary>
    /// Shot weapon loading a bullet from bullet pool
    /// </summary>
    public void ShotWeapon()
    {
        if (actualPlayerWeapon == null || !canShot)
            return;

        switch(actualPlayerWeapon.BulletType)
        {
            case BulletType.Parabolic:
                ParabolicBullet parabolicBullet = BulletPooler.Instance.GetBullet<ParabolicBullet>();
                parabolicBullet.Shot(actualPlayerWeapon.BulletSpawn);
                break;

            case BulletType.Traction:
                TractionBullet tractionBullet = BulletPooler.Instance.GetBullet<TractionBullet>();
                tractionBullet.Shot(actualPlayerWeapon.BulletSpawn);
                break;

            case BulletType.Bouncy:
                BouncyBullet bouncyBullet = BulletPooler.Instance.GetBullet<BouncyBullet>();
                bouncyBullet.Shot(actualPlayerWeapon.BulletSpawn);
                break;
        }

        StartCoroutine(ActivateNextShot());
    }

    #endregion

    #region Inner Methods

    /// <summary>
    /// Player next bullet cooldown
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActivateNextShot()
    {
        canShot = false;
        yield return new WaitForSeconds(actualPlayerWeapon.BulletCooldown);
        canShot = true;

    }

    #endregion
}
