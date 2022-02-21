using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DanceState{ Idle, HipHop, Macarena, House }

/// <summary>
/// Control character dancing animations states
/// </summary>
public class DancingCharacterController : MonoBehaviour
{
    #region Fields and properties

    [Header("Animator reference")]
    [SerializeField]
    private Animator characterAnimator;

    public static Action<DanceState> onDanceStateChanged;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        onDanceStateChanged += ChangeDanceState;
    }

    private void OnDisable()
    {
        onDanceStateChanged -= ChangeDanceState;
    }

    #endregion

    #region Inner Methods

    /// <summary>
    /// Set new character's animation state
    /// </summary>
    /// <param name="newDanceState">New dance state</param>
    private void ChangeDanceState(DanceState newDanceState) => characterAnimator.SetTrigger(newDanceState.ToString());

    #endregion

}
