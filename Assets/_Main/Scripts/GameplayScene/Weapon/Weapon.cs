using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls weapon behaviour and data
/// </summary>
public class Weapon : MonoBehaviour
{
    #region Fields and Properties

    [Header("Weapon data")]
    [SerializeField]
    private BulletType bulletType;
    [SerializeField]
    private Transform bulletSpawn;
    [SerializeField]
    private Rigidbody weaponRigidbody;
    [SerializeField]
    private SphereCollider weaponTrigger;
    [SerializeField]
    private BoxCollider weaponCollider;

    private Vector3 originalWeaponPosition;
    private Vector3 originalWeaponRotation;
    private Vector3 originalWeaponScale;
    private Transform originalParent;
    private BulletData bulletData;

    public float BulletCooldown => bulletData.bulletSecondsCooldown;
    public Transform BulletSpawn => bulletSpawn;
    public BulletType BulletType => bulletType;

    #endregion

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnablePhysics(false);
            PlayerController.onSetNewWeapon?.Invoke(this);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Set weapon bullet data and save actual weapon position as original position
    /// </summary>
    /// <param name="newBulletData">New weapon bullet datay</param>
    public void SetupWeapon(BulletData newBulletData, Transform originalParent)
    {
        EnablePhysics(true);
        bulletData = newBulletData;
        originalWeaponPosition = transform.position;
        originalWeaponRotation = transform.eulerAngles;
        originalWeaponScale = transform.localScale;
        this.originalParent = originalParent;
    }

    /// <summary>
    /// Set weapon new location and update weapon parent object
    /// </summary>
    /// <param name="newParent">New weapon parent</param>
    public void SetWeaponNewLocation(Transform newParent)
    {
        transform.parent = newParent;
        transform.position = newParent.position;
        transform.eulerAngles = newParent.eulerAngles;
        transform.localScale = newParent.localScale;
    }

    /// <summary>
    /// Set weapon in original location
    /// </summary>
    public void ResetWeaponLocation()
    {
        transform.position = originalWeaponPosition;
        transform.eulerAngles = originalWeaponRotation;
        transform.localScale = originalWeaponScale;
        transform.parent = originalParent;
        EnablePhysics(true);
    }

    #endregion

    #region Inner Methods

    /// <summary>
    /// Set custom state for physics objects
    /// </summary>
    /// <param name="state">new physics active state</param>
    private void EnablePhysics(bool state)
    {
        weaponRigidbody.useGravity = state;
        weaponTrigger.enabled = state;
        weaponCollider.enabled = state;
    }

    #endregion
}
