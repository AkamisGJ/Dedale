using UnityEngine;
using Aura2API;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SoundDataBase _soundDataBase;
    [SerializeField] private GameLoopManager _gameLoopManager;
    [SerializeField] private GameObject _cameraHolderSpawn = null;
    [SerializeField] private GameManager.MyState myState = GameManager.MyState.Game;
    [SerializeField] private PlayerAgentController _playerController = null;
    [SerializeField] private Camera _cameraPlayer = null;
    [SerializeField] private Camera _cameraUI = null;

    private void Awake()
    {
        /*if (!FindObjectOfType<PlayerManager>())
        {
            Instantiate(_gameLoopManager);
            Instantiate(_playerManager);
            Instantiate(_gameManager);
            Instantiate(_inputManager);
            Instantiate(_cameraManager);
            Instantiate(_soundDataBase);
            Instantiate(_soundManager);
            GameManager.Instance.CurrentState = GameManager.MyState.Game;
        }*/
        GameManager.Instance.CurrentState = myState;
        PlayerManager.Instance.Player = _playerController;
        PlayerManager.Instance.CameraPlayerInstantiate = _cameraPlayer;
        PlayerManager.Instance.CameraUIInstantiate = _cameraUI;
        PlayerManager.Instance.InstantiatePlayer(transform);
        Destroy(_cameraHolderSpawn);
    }
}