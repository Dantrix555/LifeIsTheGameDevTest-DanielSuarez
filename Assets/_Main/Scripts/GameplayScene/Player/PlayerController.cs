using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Main controller for the main functionality player scripts
/// </summary>
public class PlayerController : MonoBehaviour, GameInputs.ICharacterActionMapsActions
{

    #region Fields and Properties

    [Header("Player controllers")]
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerCameraLook playerRotation;
    [SerializeField]
    private PlayerWeapon playerWeapon;

    [Header("Resources path")]
    [SerializeField]
    private string characterDataPath;

    private GameInputs gameInputs;
    private CharactersStatsData characterData;

    public static Action<Weapon> onSetNewWeapon;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        //Instance input system
        gameInputs = new GameInputs();
        gameInputs.CharacterActionMaps.SetCallbacks(this);

        characterData = Resources.Load(characterDataPath) as CharactersStatsData;

        if(characterData == null)
        {
            Debug.LogError("Character data couldn't be loaded please check your resource path in player component object from " + gameObject.name);
            return;
        }

        playerMovement.CharacterData = characterData;
        playerRotation.CharacterData = characterData;
        playerRotation.SetupPlayerTransform(transform);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        gameInputs.CharacterActionMaps.Enable();
        onSetNewWeapon += playerWeapon.SetNewWeaponToPlayer;
    }

    private void OnDisable()
    {
        gameInputs.CharacterActionMaps.Disable();
        onSetNewWeapon -= playerWeapon.SetNewWeaponToPlayer;
    }

    #endregion
    
    #region Input System Implementation

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.SetMoveDirection(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerWeapon.ShotWeapon();
        }
    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        playerRotation.SetMouseXValue(context.ReadValue<float>());
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        playerRotation.SetMouseYValue(context.ReadValue<float>());
    }

    #endregion
}
