using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAgentController : MonoBehaviour
{
    #region Fields
    #region SerializedFields
    [Tooltip("Character controller of player prefab")]
    [SerializeField] private CharacterController _characterController = null;
    [Tooltip("Player Data ressource")]
    [SerializeField] private PlayerData _playerData = null;
    [Tooltip("Camera Holder will be the parent of the Camera")]
    [SerializeField] private Transform _cameraHolder = null;
    [Tooltip("Object Holder will be the parent of the observable object when player observe them")]
    [SerializeField] private Transform _objectHolder = null;
    [SerializeField] private Animator _animatorCamera = null;
    #endregion SerializedFields
    #region PrivateFields
    private Vector3 _direction = Vector3.zero;
    private Camera _mainCamera = null;
    private bool _canMove = true;
    private bool _isZoom = false;
    private bool _zooming = false;
    private bool _unZooming = false;
    private float _timeZoom = 0;
    private float _zoomLerp = 0;
    #endregion PrivateFields
    #region StatesPlayer
    public enum MyState
    {
        MOVEMENT,
        OBSERVE,
        INTERACTION,
        QTELADDER,
        NARROWWAY,
        LIANA,
        FALL,
        INFINITYCORRIDOR,
    }
    private MyState _currentState = MyState.MOVEMENT;
    private Dictionary<MyState, IPlayerState> _states = null;
    #endregion StatesPlayer
    #endregion Fields

    #region Properties
    public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
    public Transform CameraHolder { get { return _cameraHolder; } }
    public Transform ObjectHolder { get => _objectHolder; }
    public MyState CurrentState { get => _currentState; }
    public Dictionary<MyState, IPlayerState> States { get => _states; }
    public PlayerData PlayerData { get => _playerData; }
    public bool CanMove { get => _canMove; set => _canMove = value; }
    public float TimeZoom { get => _timeZoom; set => _timeZoom = value; }
    public Animator AnimatorCamera { get => _animatorCamera; set => _animatorCamera = value; }
    #endregion Properties

    private void Awake()
    {
        GameLoopManager.Instance.GameLoopPlayer += OnUpdate;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _states = new Dictionary<MyState, IPlayerState>();
        _states.Add(MyState.INTERACTION, new IInteraction());
        _states.Add(MyState.MOVEMENT, new IMouvement());
        _states.Add(MyState.OBSERVE, new IObserve());
        _states.Add(MyState.QTELADDER, new IQTELadder());
        _states.Add(MyState.NARROWWAY, new INarrowWay());
        _states.Add(MyState.LIANA, new ILiana());
        _states.Add(MyState.FALL, new IFall());
        _states.Add(MyState.INFINITYCORRIDOR, new IInfintyCorridor());
        _currentState = MyState.MOVEMENT;
        _states[MyState.INTERACTION].Init(_playerData, _mainCamera);
        _states[MyState.MOVEMENT].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.OBSERVE].Init(_playerData, _mainCamera);
        _states[MyState.QTELADDER].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.NARROWWAY].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.LIANA].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.FALL].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.INFINITYCORRIDOR].Init(_playerData, _mainCamera, _characterController);
        _states[_currentState].Enter();
        _timeZoom = 0;
        InputManager.Instance.Zoom += Zoom;
    }

    void OnUpdate()
    {
        _states[_currentState].Update();
        if (_currentState == MyState.MOVEMENT || _currentState == MyState.OBSERVE || _currentState == MyState.INFINITYCORRIDOR)
        {
            if (_zooming == true)
            {
                Zooming(1);
            }
            if (_unZooming == true)
            {
                Zooming(-1);
            }
        }
    }

    public void ChangeState(MyState nextState, Collider collider = null)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter(collider);
        _currentState = nextState;
    }

    public void Zoom(bool zoomBool)
    {
        if (_canMove == true)
        {
            if (zoomBool == true)
            {
                if (_isZoom == false && _zooming == false)
                {
                    _zooming = true;
                    _unZooming = false;
                }
            }
            if (zoomBool == false && (_zooming == true || _isZoom == true))
            {
                if (_unZooming == false)
                {
                    _zooming = false;
                    _unZooming = true;
                }
            }
        }
    }

    public void Zooming(float inversion)
    {
        _timeZoom += Time.deltaTime * inversion;
        _timeZoom = Mathf.Clamp(_timeZoom, 0, _playerData.ZoomTransition.keys[_playerData.ZoomTransition.length - 1].time);
        _zoomLerp = _playerData.FieldOfView - _playerData.ZoomTransition.Evaluate(_timeZoom) * (_playerData.FieldOfView - _playerData.ZoomFieldOfView);
        _mainCamera.fieldOfView = _zoomLerp;
        if (inversion < 0 && _zoomLerp == _playerData.FieldOfView)
        {
            _isZoom = false;
            _unZooming = false;
        }
        if (inversion > 0 && _zoomLerp == _playerData.ZoomFieldOfView)
        {
            _isZoom = true;
            _zooming = false;
        }
    }

    private void OnDestroy()
    {
        if(InputManager.Instance != null)
        {
            InputManager.Instance.Zoom -= Zoom;
        }
    }
}