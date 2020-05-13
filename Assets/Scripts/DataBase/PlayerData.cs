using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerData", menuName = "DataBase/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region Fields
    [BoxGroup("SphereCast Interaction Object", centerLabel: true)]
    [Tooltip("Layer Interaction object")]
    [SerializeField] private LayerMask _layerMask;
    [BoxGroup("SphereCast Interaction Object")]
    [Tooltip("Define the ray of the sphereCast for interact with object")]
    [SerializeField] private float _rayonInteraction;
    [BoxGroup("SphereCast Interaction Object")]
    [Tooltip("Define the distance of the sphereCast for interact with object")]
    [SerializeField] private float _maxDistanceInteractionObject = 5;
    [BoxGroup("SphereCast Interaction Object")]
    [Tooltip("Define the angle when the player has the information of what object is interactive")]
    [SerializeField] private float _angleHelper;
    [BoxGroup("SphereCast Interaction Object")]
    [Tooltip("Define the distance when the player has the information of what interaction object is interactive")]
    [SerializeField] private float _distanceHelperInteraction;
    [BoxGroup("SphereCast Interaction Object")]
    [Tooltip("Define the distance when the player has the information of what observable object is interactive")]
    [SerializeField] private float _distanceHelperObservableObject;

    [BoxGroup("Interaction Object Helper", centerLabel: true)]
    [Tooltip("The canvas for interaction helper")]
    [SerializeField] private Canvas _canvasHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for input helper")]
    [SerializeField] private RawImage _inputHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for interaction helper")]
    [SerializeField] private RawImage _interactionHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for obsvervable object helper")]
    [SerializeField] private RawImage _observableObjectHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for liana helper")]
    [SerializeField] private RawImage _lianaHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for narrow way helper")]
    [SerializeField] private RawImage _narrowWayHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for ladder helper")]
    [SerializeField] private RawImage _ladderHelper;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("The image for locked door")]
    [SerializeField] private RawImage _lock;
    [BoxGroup("Interaction Object Helper")]
    [Tooltip("All Layer that block Interaction and interaction helper")]
    [SerializeField] private LayerMask _cantSeeInteractionHelperBehindThis;


    [BoxGroup("Sprint")]
    [Tooltip("0 to 1 value and 0 to infinity time")]
    [SerializeField] private AnimationCurve _accelerationCurve = null;
    [BoxGroup("Crouch")]
    [Tooltip("0 to 1 value and 0 to infinity time")]
    [SerializeField] private AnimationCurve _crouchCurve = null;

    [BoxGroup("Character Controller", centerLabel: true)]
    [Tooltip("override the slope limit in character controller")]
    [SerializeField] private float _slopeLimit = 45;
    [BoxGroup("Character Controller")]
    [Tooltip("override the Step Offset in character controller")]
    [SerializeField] private float _stepOffset = 0.3f;

    [BoxGroup("Move speed",centerLabel: true)]
    [Tooltip("Move speed of forward direction")]
    [SerializeField] private float _speedForward = 1;
    [BoxGroup("Move speed")]
    [Tooltip("Move speed of back direction")]
    [SerializeField] private float _speedBack = 0.5f;
    [BoxGroup("Move speed")]
    [Tooltip("Move speed of left and right direction")]
    [SerializeField] private float _speedSide = 0.5f;
    [BoxGroup("Move speed")]
    [Tooltip("Move speed on ladder")]
    [SerializeField] private float _speedLadder = 0.5f;
    [BoxGroup("Move speed")]
    [Tooltip("global direction multiplier speed of player (0 to infini)")]
    [SerializeField] private float _gloabalSpeed = 1;
    [BoxGroup("Move speed")]
    [Tooltip("Move speed when crouching")]
    [SerializeField] private float _crouchMoveSpeed = 1;
    [BoxGroup("Move speed")]
    [Tooltip("Time between to step")]
    [Range(0, 0.1f)]
    [SerializeField] private float _timeStep = 0.5f;

    [BoxGroup("Sprint", centerLabel: true)]
    [Tooltip("Multiply forward speed by this value")]
    [SerializeField] private float _maxSprintSpeed = 2;

    [BoxGroup("Mouse Sensitivity", centerLabel: true)]
    [Tooltip("Mousse sensitivity when player observe an object")]
    [SerializeField] private float _mouseSensitivityObserve = 1.0f;

    [BoxGroup("Player's Camera", centerLabel: true)]
    [Tooltip("Define the max angle on X of the player's camera, auto inverse this value to define the min angle")]
    [SerializeField] private float _angleX = 60f;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define distance of camera slow when player stop move it")]
    [SerializeField] private float _stackMovement = 0.5f;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define speed of camera slow when player stop move it")]
    [SerializeField] private float _speedToStopCamera = 2;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define the max angle on Y of the player's camera when in infinity corridor, auto inverse this value to define the min angle")]
    [SerializeField] private float _angleY = 40f;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define the min angle on Y of the player's camera when on ladder")]
    [SerializeField] private float _ladderMinAngleY = 40f;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define the angle on Y of the player's camera when on ladder")]
    [SerializeField] private float _ladderAngleY = 80f;
    [BoxGroup("Player's Camera")]
    [Tooltip("Define the reduction of camera speed when player control door")]
    [SerializeField] private float _speedCameraDoor = 0.0000000000000001f;

    [BoxGroup("Mouse Sensitivity")]
    [Tooltip("Mousse sensitivity on X axis when player move")]
    [SerializeField] private float _sensitivityMouseX = 1f;
    [BoxGroup("Mouse Sensitivity")]
    [Tooltip("Mousse sensitivity on Y axis when player move")]
    [SerializeField] private float _sensitivityMouseY = 1f;

    [BoxGroup("Sprint")]
    [Tooltip("Sprint max time in seconds (0 = infinty)")]
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
    [Tooltip("Define the normal field of view")]
    [SerializeField] private float _zoomFieldOfView = 60;
    [BoxGroup("Camra Zoom")]
    [Tooltip("0 to 1 value and 0 to infinity time")]
    [SerializeField] private AnimationCurve _zoomTransition = null;

    [BoxGroup("Crouch", centerLabel: true)]
    [Tooltip("The normal height of player")]
    [SerializeField] private float _maxHeight = 2;
    [BoxGroup("Crouch")]
    [Tooltip("The difference between crouch height and normal height")]
    [SerializeField] private float _differenceHeightCrouch = 1;

    [BoxGroup("Color Hightlight")]
    [Tooltip("Define the color of the interactible object when the player look at it")]
    [SerializeField] private Color _colorHightlightObject;


    [BoxGroup("Narrow Way", centerLabel: true)]
    [Tooltip("The max angle of camera in narrow way")]
    [SerializeField] private float _maxAngleNarrowWay = 60;
    [BoxGroup("Narrow Way")]
    [Tooltip("The speed of player's camera rotation")]
    [SerializeField] private float _speedRotationCamera = 2;
    [BoxGroup("Narrow Way")]
    [Tooltip("The speed of player when enter or exit Narrow Way")]
    [SerializeField] private float _speedEnterExitNarrowWay = 2;
    [BoxGroup("Narrow Way")]
    [Tooltip("The speed of player in narrow way")]
    [SerializeField] private float _speedNarrowWay = 2;

    [BoxGroup("Canvas", centerLabel: true)]
    [Tooltip("The canvas when loading the next scene")]
    [SerializeField] private Canvas _LoadingCanvas = null;

    #endregion Fields

    #region Properties
    public AnimationCurve AccelerationCurve { get { return _accelerationCurve; } }
    public AnimationCurve CrouchCurve { get { return _crouchCurve; } }
    public float SpeedForward { get { return _speedForward; } }
    public float SpeedBack { get { return _speedBack; } }
    public float SpeedSide { get { return _speedSide; } }
    public float GlobalSpeed { get { return _gloabalSpeed; } }
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
    public float DifferenceHeightCrouch { get => _differenceHeightCrouch; }
    public float MaxHeight { get => _maxHeight; }
    public float ZoomFieldOfView { get => _zoomFieldOfView; }
    public float MaxDistanceInteractionObject { get => _maxDistanceInteractionObject; }
    public float SpeedLadder { get => _speedLadder; }
    public float SpeedNarrowWay { get => _speedNarrowWay; }
    public float MaxAngleNarrowWay { get => _maxAngleNarrowWay; }
    public Color ColorHightLightObject { get => _colorHightlightObject; }
    public float RayonInteraction { get => _rayonInteraction; }
    public float DistanceHelperInteraction { get => _distanceHelperInteraction; }
    public float AngleHelper { get => _angleHelper; }
    public RawImage InputHelper { get => _inputHelper; }
    public RawImage InteractionHelper { get => _interactionHelper; }
    public RawImage ObservableObjectHelper { get => _observableObjectHelper; }
    public RawImage LianaHelper { get => _lianaHelper; }
    public RawImage NarrowWayHelper { get => _narrowWayHelper; }
    public RawImage LadderHelper { get => _ladderHelper; }
    public Canvas CanvasHelper { get => _canvasHelper; }
    public float DistanceHelperObservableObject { get => _distanceHelperObservableObject; }
    public RawImage Lock { get => _lock; }
    public float SpeedEnterExitNarrowWay { get => _speedEnterExitNarrowWay; }
    public float SpeedRotationCamera { get => _speedRotationCamera; }
    public float SpeedToStopCamera { get => _speedToStopCamera; }
    public float StackMovement { get => _stackMovement; }
    public float TimeStep { get => _timeStep; }
    public float CrouchMoveSpeed { get => _crouchMoveSpeed; }
    public float AngleY { get => _angleY; }
    public LayerMask CantSeeInteractionHelperBehindThis { get => _cantSeeInteractionHelperBehindThis; }
    public float LadderMinAngleY { get => _ladderMinAngleY; }
    public float LadderAngleY { get => _ladderAngleY; }
    public float SpeedCameraDoor { get => _speedCameraDoor; }
    public Canvas LoadingCanvas { get => _LoadingCanvas; }
    #endregion Properties
}