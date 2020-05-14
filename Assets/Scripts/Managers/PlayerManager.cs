using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Fields
    private PlayerAgentController _playerGameObject = null;
    private Camera _cameraPlayerGameobject = null;
    private Camera _cameraUIGameObject = null;
    private bool _haveKey = false;
    private PlayerAgentController.MyState _startState = PlayerAgentController.MyState.MOVEMENT;

    [SerializeField] private PlayerAgentController _playerController = null;
    [SerializeField] private Camera _cameraPlayer = null;

    [SerializeField] private Camera _cameraUI = null;
    private bool _isInNarrowWay = false;
    private bool _playerIsDead = false;
    #endregion Fields

    #region Properties
    public PlayerAgentController Player { get { return _playerController; } set { _playerController = value; } }

    public PlayerAgentController PlayerController { get { return _playerGameObject; } set { _playerGameObject = value; } }

    public Camera CameraUIInstantiate { get { return _cameraUI; } set { _cameraUI = value; } }

    public Camera CameraPlayerInstantiate { get { return _cameraPlayer; } set { _cameraPlayer = value; } }

    public bool PlayerIsDead { get { return _playerIsDead; } set { _playerIsDead = value; } }

    public Camera CameraPlayer { get => _cameraPlayerGameobject; }
    public Camera CameraUI { get => _cameraUIGameObject; }
    public bool HaveKey { get => _haveKey; set => _haveKey = value; }
    public bool IsInNarrowWay { get => _isInNarrowWay; set => _isInNarrowWay = value; }
    public PlayerAgentController.MyState StartState { get => _startState; set => _startState = value; }
    #endregion Properties

    protected override void Awake()
    {
        base.Awake();
    }

    public void DestroyThis(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void InstantiatePlayer(Transform tranformSpawnerPlayer)
    {
        _cameraUIGameObject = Instantiate(_cameraUI, new Vector3(0, 0, 0), Quaternion.identity);
        CameraManager.Instance.CameraUI = _cameraUIGameObject;
        _playerIsDead = false;
        _playerGameObject = Instantiate(_playerController, tranformSpawnerPlayer.position, Quaternion.identity);
        _cameraPlayerGameobject = Instantiate(_cameraPlayer, _playerGameObject.CameraHolder.position, _playerGameObject.transform.rotation, _playerGameObject.CameraHolder);
        CameraManager.Instance.CameraPlayer = _cameraPlayerGameobject;
        _playerGameObject.MainCamera = _cameraPlayerGameobject;
        _playerGameObject.transform.Rotate(tranformSpawnerPlayer.rotation.eulerAngles);
    }
}