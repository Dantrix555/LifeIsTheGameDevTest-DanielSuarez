using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls weapon in gameplay scene
/// </summary>
public class WeaponsController : MonoBehaviour
{
    #region Fields and Properties

    [Header("Weapons and bullets components")]
    [SerializeField]
    private Weapon[] sceneWeapons;

    #endregion

    #region Unity Methods

    private void Start()
    {
        for (int i = 0; i < sceneWeapons.Length; i++)
        {
            BulletData data = BulletPooler.Instance.BulletsData.bulletsData.Find(bullet => bullet.bulletType == sceneWeapons[i].BulletType);
            sceneWeapons[i].SetupWeapon(data, transform);
        }
    }

    #endregion
}
