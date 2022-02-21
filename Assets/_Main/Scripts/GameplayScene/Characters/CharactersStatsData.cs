using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the values for the character main stats
/// </summary>
[CreateAssetMenu(fileName = "New Character Stats Data", menuName = "Life is the game test/CharactersStatsData", order = 1)]
[Serializable]
public class CharactersStatsData : ScriptableObject
{
    [Header("Character Dance State")]
    public DanceState characterDanceState;

    [Header("Physics values")]
    [Range(5f, 15f)]
    public float moveSpeed;
    [Range(3f, 7f)]
    public float extraSprintSpeed;
    [Range(7f, 15f)]
    public float mouseMoveSensitive;
    [Range(80f, 100f)]
    public float characterRotationValue;
}
