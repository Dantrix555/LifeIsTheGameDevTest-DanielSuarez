using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movement values and physics
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region Feilds and Properties

    [Header("Physic components")]
    [SerializeField]
    private CharacterController characterController;

    private Vector2 inputDirection;
    private CharactersStatsData characterData;

    public CharactersStatsData CharacterData { get => characterData; set => characterData = value; }

    #endregion

    #region Unity Methods

    // Update is called once per frame
    private void Update()
    {
        Vector3 moveDirection = inputDirection.x * transform.right + inputDirection.y * transform.forward;
        characterController.Move(moveDirection * characterData.moveSpeed * Time.deltaTime);
    }

    #endregion

    #region Public Methods
    
    /// <summary>
    /// Set movement axis values (Specificly from input system callbacks)
    /// </summary>
    /// <param name="axis">Custom movement axis</param>
    public void SetMoveDirection(Vector2 axis) => inputDirection = axis;

    #endregion
}
