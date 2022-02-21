using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Traction bullet controller
/// </summary>
public class TractionBullet : Bullet
{
    #region Fields and Properties

    private SphereCollider tractionRadius;
    private List<Transform> tractedObjects;
    private float bulletRandomRotationValue;

    #endregion

    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == bulletData.InteractionLayer)
            tractedObjects.Add(other.transform);
    }

    private void LateUpdate()
    {
        for (int i = 0; i < tractedObjects.Count; i++)
        {
            tractedObjects[i].transform.position = transform.position + (tractedObjects[i].position - transform.position).normalized * 7f;
            tractedObjects[i].transform.RotateAround(transform.position, new Vector3(Random.Range(0, 45), Random.Range(0, 45), Random.Range(0, 45)), 50f * Time.deltaTime);
        }
    }

    #endregion

    #region Bullet Inheritance

    public override void Shot(Transform shotOrigin)
    {
        gameObject.SetActive(true);
        IsInUse = true;
        transform.position = shotOrigin.position;
        bulletRigidbody.velocity = shotOrigin.transform.forward * bulletData.bulletSpeed;
        tractionRadius = GetComponent<SphereCollider>();
        tractedObjects = new List<Transform>();
        tractionRadius.radius = 2f;
        Invoke("DeactivateBullet", bulletData.spawnTime);
    }

    protected override void DeactivateBullet()
    {
        tractionRadius.radius = 0.5f;
        tractedObjects.Clear();
        tractedObjects = new List<Transform>();
        base.DeactivateBullet();
    }

    #endregion
}
