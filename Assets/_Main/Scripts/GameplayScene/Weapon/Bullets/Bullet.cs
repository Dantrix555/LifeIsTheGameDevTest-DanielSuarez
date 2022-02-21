using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that set and controls the basic behaviour of any kind of bullet
/// </summary>
public abstract class Bullet : MonoBehaviour
{

    #region Fields and Properties

    protected Rigidbody bulletRigidbody;
    private MeshRenderer bulletRenderer;

    protected BulletData bulletData;
    private bool isInUse;
    private GameObject actualPrefabInstance;

    public bool IsInUse { get => isInUse; protected set => isInUse = value; }

    #endregion

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != bulletData.InteractionLayer)
            DeactivateBullet();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Set bullet new data and update some physic and visual values as scale or basic material color
    /// </summary>
    /// <param name="newData">New bullet data to set</param>
    public virtual void SetBulletData(BulletData newData)
    {
        gameObject.SetActive(false);
        bulletData = newData;
        transform.localScale = Vector3.one * bulletData.bulletScale;
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRenderer = GetComponentInChildren<MeshRenderer>();
        bulletRenderer.material.color = bulletData.bulletColor;
        bulletRigidbody.useGravity = bulletData.bulletHasGravity;
        actualPrefabInstance = Instantiate(bulletData.VFXPrefab, transform);
        isInUse = false;
    }

    public void DestroyBulletReference()
    {
        if(actualPrefabInstance != null)
            Destroy(actualPrefabInstance);
        Destroy(this);
    }

    /// <summary>
    /// Shot bullet behaviour to implement in child class
    /// </summary>
    /// <param name="shotOrigin"></param>
    public abstract void Shot(Transform shotOrigin);

    #endregion

    #region Protected Methods

    /// <summary>
    /// Deactivate the bullet and it's free for use
    /// </summary>
    protected virtual void DeactivateBullet()
    {
        VFXExplosionPooler.Instance.SetExplosionVFX(transform);
        isInUse = false;
        gameObject.SetActive(false);
    }

    #endregion
}
