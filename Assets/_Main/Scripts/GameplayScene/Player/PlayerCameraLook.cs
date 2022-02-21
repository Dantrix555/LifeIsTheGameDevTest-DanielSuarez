using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player camera rotation
/// </summary>
public class PlayerCameraLook : MonoBehaviour
{

    #region Fields and Properties

    private Transform playerTransform;
    private Vector2 rotationInput;
    private float xRotation;
    private CharactersStatsData characterData;
    private bool hasPlayerReference = false;

    public CharactersStatsData CharacterData { get => characterData; set => characterData = value; }

    #endregion

    #region Unity Methods

    private void Update()
    {
        if (!hasPlayerReference)
            return;

        float mouseX = rotationInput.x * characterData.mouseMoveSensitive * Time.deltaTime;
        float mouseY = rotationInput.y * characterData.mouseMoveSensitive * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -characterData.characterRotationValue, characterData.characterRotationValue);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    #endregion

    #region Public Methods
    
    /// <summary>
    /// Set player new transform component reference to control its Y axis rotation
    /// </summary>
    /// <param name="playerTransform">new Player transform</param>
    public void SetupPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        xRotation = 0f;
        hasPlayerReference = true;
    }

    /// <summary>
    /// Set mouse X delta value
    /// </summary>
    /// <param name="xAxis">Actual x delta value</param>
    public void SetMouseXValue(float xAxis) => rotationInput.x = xAxis;

    /// <summary>
    /// Set mouse Y delta value
    /// </summary>
    /// <param name="yAxis">Actual y delta value</param>
    public void SetMouseYValue(float yAxis) => rotationInput.y = yAxis;

    #endregion
}
