using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "DataBase/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region Fields
    [SerializeField] private AnimationCurve _accelerationCurve = null;
    [SerializeField] private AnimationCurve _crouchCurve = null;
    [SerializeField] private float _accelerationTime = 0.0f;
    [SerializeField] private float _timeMultiplier = 1.0f;
    [SerializeField] private float _speedForward = 1;
    [SerializeField] private float _speedBack = 0.5f;
    [SerializeField] private float _moveSpeedSide = 0.5f;
    [SerializeField] private float _moveSpeedMultiplier = 1;
    [SerializeField] private float _maxSprintSpeed = 2;
    [SerializeField] private float _mouseSensitivityInteract = 1.0f;
    [SerializeField] private float _angleX = 60f;
    [SerializeField] private float _sensitivityMouseX = 1f;
    [SerializeField] private float _sensitivityMouseY = 1f;
    [SerializeField] float _speedSprintMax = 1;
    [SerializeField] float _sprintTimeMax = 100;
    [SerializeField] float _gravity = 9;
    [SerializeField] float _maxGravity = 45;
    #endregion Fields

    #region Properties
    public AnimationCurve AccelerationCurve { get { return _accelerationCurve; } }
    public AnimationCurve CrouchCurve { get { return _crouchCurve; } }
    public float AccelerationTime { get { return _accelerationTime; } }
    public float TimeMultiplier { get { return _timeMultiplier; } }
    public float SpeedForward { get { return _speedForward; } }
    public float SpeedBack { get { return _speedBack; } }
    public float MoveSpeedSide { get { return _moveSpeedSide; } }
    public float MoveSpeedMultiplier { get { return _moveSpeedMultiplier; } }
    public float MaxSprintSpeed { get { return _maxSprintSpeed; } }
    public float MouseSensitivityInteract { get { return _mouseSensitivityInteract; } }
    public float AngleX { get { return _angleX; } }
    public float SensitivityMouseX { get { return _sensitivityMouseX; } }
    public float SensitivityMouseY { get { return _sensitivityMouseY; } }
    public float SpeedSprintMax { get { return _speedSprintMax; } }
    public float SprintTimeMax { get { return _sprintTimeMax; } }
    public float Gravity { get => _gravity; }
    public float MaxGravity { get => _maxGravity; }
    #endregion Properties
}
