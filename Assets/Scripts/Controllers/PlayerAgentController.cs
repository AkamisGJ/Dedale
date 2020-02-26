using System.Collections.Generic;
using UnityEngine;

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
    [Tooltip("Audio source of the player")]
    [SerializeField] private AudioSource _audioSourcePlayer = null;
    [Tooltip("Animator of player")]
    [SerializeField] private Animator _animatorShadow = null;
    #endregion SerializedFields
    #region PrivateFields
    private Vector3 _direction = Vector3.zero;
    private Camera _mainCamera = null;
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
    }
    private MyState _currentState = MyState.MOVEMENT;
    private Dictionary<MyState, IPlayerState> _states = null;
    #endregion StatesPlayer
    #endregion Fields

    #region Properties
    public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
    public Transform CameraHolder { get { return _cameraHolder; } }
    public Transform ObjectHolder { get => _objectHolder; }
    public AudioSource AudioSourcePlayer { get => _audioSourcePlayer; set => _audioSourcePlayer = value; }
    public MyState CurrentState { get => _currentState; }
    public Dictionary<MyState, IPlayerState> States { get => _states; }
    public PlayerData PlayerData { get => _playerData; }
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
        _currentState = MyState.MOVEMENT;
        _states[MyState.INTERACTION].Init(_playerData, _mainCamera);
        _states[MyState.MOVEMENT].Init(_playerData, _mainCamera, _characterController, _animatorShadow);
        _states[MyState.OBSERVE].Init(_playerData, _mainCamera);
        _states[MyState.QTELADDER].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.NARROWWAY].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.LIANA].Init(_playerData, _mainCamera, _characterController);
        _states[MyState.FALL].Init(_playerData, _mainCamera, _characterController);
        _states[_currentState].Enter();
    }

    void OnUpdate()
    {
        _states[_currentState].Update();
    }

    public void ChangeState(MyState nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }
}
