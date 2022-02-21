using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object with custom bullets data
/// </summary>
[CreateAssetMenu(fileName = "New Bullets Data", menuName = "Life is the game test/Bullets Data", order = 0)]
[Serializable]
public class BulletsData : ScriptableObject
{
    public List<BulletData> bulletsData;
}

/// <summary>
/// Contains specific bullet data
/// </summary>
[Serializable]
public class BulletData
{
    [Header("Id data")]
    public BulletType bulletType;

    [Header("Physics data")]
    public float bulletSpeed;
    public float bulletMaxHeight;
    public float bulletScale;
    public bool bulletHasGravity;
    public bool bulletAtractObjects;
    public int InteractionLayer;

    [Header("Visualization data")]
    public float spawnTime;
    public float bulletSecondsCooldown;
    public Color bulletColor;
    public GameObject VFXPrefab;
}

[Serializable]
public enum BulletType { Parabolic, Traction, Bouncy }