using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls character dance and game start
/// </summary>
public class SelectDancePanel : MonoBehaviour
{
    #region Fields and properties

    [Header("Panel buttons references")]
    [SerializeField]
    private Button hipHopButton;
    [SerializeField]
    private Button macarenaButton;
    [SerializeField]
    private Button houseButton;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Animator backgroundAnimator;

    [Header("Main Character Scriptable object")]
    [SerializeField]
    private CharactersStatsData characterData;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        hipHopButton.onClick.AddListener(() => { SetDanceStateFromButton(DanceState.HipHop); });
        macarenaButton.onClick.AddListener(() => { SetDanceStateFromButton(DanceState.Macarena); });
        houseButton.onClick.AddListener(() => { SetDanceStateFromButton(DanceState.House); });
        startGameButton.interactable = false;
        startGameButton.onClick.AddListener(StartGame);
        
        DancingCharacterController.onDanceStateChanged?.Invoke(DanceState.Idle);
    }

    #endregion

    #region Inner Methods

    /// <summary>
    /// Set a custom dance as actual dance
    /// </summary>
    /// <param name="newState">new dance to set</param>
    private void SetDanceStateFromButton(DanceState newState)
    {
        startGameButton.interactable = true;
        characterData.characterDanceState = newState;
        DancingCharacterController.onDanceStateChanged?.Invoke(newState);
    }

    /// <summary>
    /// Load Main game
    /// </summary>
    private void StartGame()
    {
        backgroundAnimator.SetTrigger("FadeIn");
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    #endregion
}
