using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentController : MonoBehaviour
{
    #region Fields
    #region SerializedFields
    [SerializeField] private NavMeshAgent _playerNavMeshAgent = null;
    [SerializeField] private PlayerData _playerData = null;
    [SerializeField] private Transform _cameraHolder = null;
    [SerializeField] private Transform _objectHolder = null;
    [SerializeField] private AudioSource _audioSourcePlayer = null;
    #endregion SerializedFields
    #region PrivateFields
    private Vector3 _direction = Vector3.zero;
    private Camera _mainCamera = null;
    #endregion PrivateFields
    #region StatesPlayer
    public enum MyState
    {
        Mouvement,
        Observe,
        Interaction,
        QTE,
    }
    private MyState _currentState = MyState.Mouvement;
    private Dictionary<MyState, IPlayerState> _states = null;
    #endregion StatesPlayer
    #endregion Fields

    #region Properties
    public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
    public Transform CameraHolder { get { return _cameraHolder; } }
    public Transform ObjectHolder { get => _objectHolder; }
    public AudioSource AudioSourcePlayer { get => _audioSourcePlayer; set => _audioSourcePlayer = value; }
    #endregion Properties

    private void Awake()
    {
        GameLoopManager.Instance.StartPlayer += OnStart;
        GameLoopManager.Instance.GameLoopPlayer += OnUpdate;
    }

    void OnStart()
    {
        _states = new Dictionary<MyState, IPlayerState>();
        _states.Add(MyState.Interaction, new IInteraction());
        _states.Add(MyState.Mouvement, new IMouvement());
        _states.Add(MyState.Observe, new IObserve());
        _states.Add(MyState.QTE, new IQTE());
        _currentState = MyState.Mouvement;
        _states[MyState.Interaction].Init(_playerData, _mainCamera);
        _states[MyState.Mouvement].Init(_playerData, _mainCamera, _playerNavMeshAgent);
        _states[MyState.Observe].Init(_playerData, _mainCamera);
        _states[_currentState].Enter(this.gameObject);
    }

    void OnUpdate()
    {
        _states[_currentState].Update();
    }

    public void ChangeState(MyState nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter(gameObject);
        _currentState = nextState;
    }
}
