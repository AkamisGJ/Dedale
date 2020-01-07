using UnityEngine;
using Prof.Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Fields
    private GameObject _playerGameObject = null;
    private Camera _cameraPlayerGameobject = null;
    private Camera _cameraUIGameObject = null;

    [SerializeField] private GameObject _player = null;
    [SerializeField] private Camera _cameraPlayer = null;

    [SerializeField] private Camera _cameraUI = null;
    private Vector3 _positionCamera = Vector3.zero;
    [SerializeField] private float _offsetYCamera = 0.5f;
    [SerializeField] private float _offsetZCamera = -0.3f;

    private bool _playerIsDead = false;
    #endregion Fields

    #region Properties
    public GameObject Player { get { return _playerGameObject; } }

    public Camera CameraUIGameObject { get { return _cameraUIGameObject; } }

    public Camera CameraPlayer { get { return _cameraPlayer; } }

    public bool PlayerIsDead { get { return _playerIsDead; } set { _playerIsDead = value; } }
    #endregion Properties

    public void InstantiatePlayer(Transform tranformSpawnerPlayer)
    {
        _cameraUIGameObject = Instantiate(_cameraUI, new Vector3(0,10,0), Quaternion.identity);
        CameraManager.Instance.CameraUI = _cameraUIGameObject;
        _playerIsDead = false;
        _playerGameObject = Instantiate(_player, tranformSpawnerPlayer.position, Quaternion.identity);
        _positionCamera = tranformSpawnerPlayer.position;
        _positionCamera.y += _offsetYCamera;
        _positionCamera.z += _offsetZCamera;
        _cameraPlayerGameobject = Instantiate(_cameraPlayer, _positionCamera, Player.transform.rotation, _playerGameObject.transform);
        CameraManager.Instance.CameraPlayer = _cameraPlayerGameobject;
        PlayerController playerController = _playerGameObject.GetComponent<PlayerController>();
        playerController.MainCamera =_cameraPlayerGameobject;
    }
}