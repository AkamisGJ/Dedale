using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private SoundDataBase _soundDataBase;

    private void Awake()
    {
        if (!FindObjectOfType<PlayerManager>())
        {
            Instantiate(_playerManager);
            Instantiate(_gameManager);
            Instantiate(_inputManager);
            Instantiate(_cameraManager);
            Instantiate(_soundDataBase);
            Instantiate(_soundManager);
            GameManager.Instance.CurrentState = GameManager.MyState.Game;
        }
        PlayerManager.Instance.InstantiatePlayer(transform);
    }
}