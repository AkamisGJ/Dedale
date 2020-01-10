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
        PlayerController playerController = _playerGameObject.GetComponent<PlayerController>();
        _cameraPlayerGameobject = Instantiate(_cameraPlayer, playerController.CameraHolder.position, Player.transform.rotation, playerController.CameraHolder);
        CameraManager.Instance.CameraPlayer = _cameraPlayerGameobject;
        playerController.MainCamera =_cameraPlayerGameobject;
    }
}