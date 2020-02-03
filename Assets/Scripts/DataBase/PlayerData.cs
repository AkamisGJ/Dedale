using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerData", menuName = "DataBase/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region Fields
    [BoxGroup("Layer Mask", centerLabel: true)]
    [Tooltip("Layer Interaction player - object")]
    [SerializeField] private LayerMask _layerMask;
    [BoxGroup("Curves", centerLabel: true)]
    [Tooltip("0 to 1 value and 0 to infinity time")]
    [SerializeField] private AnimationCurve _accelerationCurve = null;
    [BoxGroup("Curves")]
    [Tooltip("2 or max height of player to 1 or crouched height of player and 0 to infinity time")]
    [SerializeField] private AnimationCurve _crouchCurve = null;

    [BoxGroup("Character Controller", centerLabel: true)]
    [Tooltip("override the slope limit in character controller")]
    [SerializeField] private float _slopeLimit = 45;
    [BoxGroup("Character Controller")]
    [Tooltip("override the Step Offset in character controller")]
    [SerializeField] private float _stepOffset = 0.3f;

    [BoxGroup("Coefficient (0 to 1)",centerLabel: true)]
    [Tooltip("Apply a coefficient to the speed of forward direction")]
    [SerializeField] private float _coefSpeedForward = 1;
    [BoxGroup("Coefficient (0 to 1)")]
    [Tooltip("Apply a coefficient to the speed of back direction")]
    [SerializeField] private float _coefSpeedBack = 0.5f;
    [BoxGroup("Coefficient (0 to 1)")]
    [Tooltip("Apply a coefficient to the speed of left and right direction")]
    [SerializeField] private float _coefSpeedSide = 0.5f;

    [BoxGroup("Move Speed", centerLabel: true)]
    [Tooltip("Multiply normalized vector direction of player by this value")]
    [SerializeField] private float _moveSpeed = 1;

    [BoxGroup("Sprint", centerLabel: true)]
    [Tooltip("Multiply forward speed by this value")]
    [SerializeField] private float _maxSprintSpeed = 2;

    [BoxGroup("Mouse Sensitivity", centerLabel: true)]
    [Tooltip("Mousse sensitivity when player observe an object")]
    [SerializeField] private float _mouseSensitivityObserve = 1.0f;

    [BoxGroup("Angle of Player's Camera", centerLabel: true)]
    [Tooltip("Define the max angle on X of the player's camera, auto inverse this value to define the min angle")]
    [SerializeField] private float _angleX = 60f;

    [BoxGroup("Mouse Sensitivity")]
    [Tooltip("Mousse sensitivity on X axis when player move")]
    [SerializeField] private float _sensitivityMouseX = 1f;
    [BoxGroup("Mouse Sensitivity")]
    [Tooltip("Mousse sensitivity on Y axis when player move")]
    [SerializeField] private float _sensitivityMouseY = 1f;

    [BoxGroup("Sprint")]
    [Tooltip("Sprint max time in seconds")]
    [SerializeField] float _sprintTimeMax = 100;

    [BoxGroup("Gravity", centerLabel: true)]
    [Tooltip("Default gravity")]
    [SerializeField] float _gravity = 9;
    [BoxGroup("Gravity")]
    [Tooltip("Gravity max when player fall")]
    [SerializeField] float _maxFallGravity = 45;

    [BoxGroup("Camra Zoom", centerLabel: true)]
    [Tooltip("Define the normal field of view")]
    [SerializeField] private float _FieldOfView = 60;
    [BoxGroup("Camra Zoom")]
    [Tooltip("0 to zoom field of view value and 0 to infinity time")]
    [SerializeField] private AnimationCurve _zoomTransition = null;

    #endregion Fields

    #region Properties
    public AnimationCurve AccelerationCurve { get { return _accelerationCurve; } }
    public AnimationCurve CrouchCurve { get { return _crouchCurve; } }
    public float CoefSpeedForward { get { return _coefSpeedForward; } }
    public float CoefSpeedBack { get { return _coefSpeedBack; } }
    public float CoefSpeedSide { get { return _coefSpeedSide; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float MaxSprintSpeed { get { return _maxSprintSpeed; } }
    public float MouseSensitivityInteract { get { return _mouseSensitivityObserve; } }
    public float AngleX { get { return _angleX; } }
    public float SensitivityMouseX { get { return _sensitivityMouseX; } }
    public float SensitivityMouseY { get { return _sensitivityMouseY; } }
    public float SprintTimeMax { get { return _sprintTimeMax; } }
    public float Gravity { get => _gravity; }
    public float MaxFallGravity { get => _maxFallGravity; }
    public AnimationCurve ZoomTransition { get => _zoomTransition; }
    public float StepOffset { get => _stepOffset; }
    public float SlopeLimit { get => _slopeLimit; }
    public LayerMask LayerMask { get => _layerMask; }
    public float FieldOfView { get => _FieldOfView; }
    #endregion Properties
}
