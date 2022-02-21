using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXExplosionPooler : BASESingleton<VFXExplosionPooler>
{
    protected VFXExplosionPooler() { }

    #region Fields and Properties

    [Header("VFX Explosion components")]
    [SerializeField]
    private List<GameObject> explosionsVFXs;
    [SerializeField]
    private GameObject explosionPrefab;

    #endregion

    #region Unity Methods

    private void Start()
    {
        explosionsVFXs.ForEach(explosion => explosion.SetActive(false));
    }

    #endregion

    #region Public Methods

    public void SetExplosionVFX(Transform newExplosionTransform)
    {
        GameObject explosion = explosionsVFXs.Find(explosionObject => !explosionObject.activeInHierarchy);
        if(explosion == null)
        {
            explosion = Instantiate(explosionPrefab, transform);
            explosion.SetActive(false);
            explosionsVFXs.Add(explosion);
        }
        explosion.transform.position = newExplosionTransform.position;
        explosion.SetActive(true);
        StartCoroutine(DeactivateExplosion(explosion));
    }

    #endregion

    #region Private Methods

    private IEnumerator DeactivateExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(1f);
        explosion.SetActive(false);
    }

    #endregion
}
