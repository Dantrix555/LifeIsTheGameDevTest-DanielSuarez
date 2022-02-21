using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls main game HUD
/// </summary>
public class HUDPanelController : MonoBehaviour
{
    #region Field and Properties

    [Header("HUD components")]
    [SerializeField]
    private CharactersStatsData characterData;

    #endregion

    #region Unity Methods

    void Start()
    {
        if (characterData != null)
            DancingCharacterController.onDanceStateChanged?.Invoke(characterData.characterDanceState);
        else
            Debug.LogError("Please check if character stats data scriptable object is referenced in " + gameObject.name);
    }

    #endregion
}
