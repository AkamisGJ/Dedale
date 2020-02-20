using UnityEngine;

public class IFall : IPlayerState
{
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private PlayerAgentController _playerController = null;
    private Vector3 _directionHorizontal = Vector3.zero;
    private Vector3 _orientation = Vector3.zero;
    private float _lerpStartPositionPlayer = 0;
    private float _lerpExitNarrowWay = 0;
    private float _speedRotateCamera = 5;
    [SerializeField] private Transform _enterTransform = null;
    private CharacterController _characterController = null;
    private float _currentGravity = 0;

    public void Init(PlayerData playerData, Camera camera, CharacterController playerController = null, Animator animator = null)
    {
        _playerData = playerData;
        _playerController = playerController;
        _characterController = _playerController.GetComponent<CharacterController>();
        _camera = camera;
    }

    public void Enter()
    {
        GameLoopManager.Instance.LoopQTE += OnStartFalling;
    }

    public void Update()
    {
        _characterController.Move(Vector3.up * _playerData.Gravity);
    }

    public void Exit()
    {

    }

    private void OnStartFalling()
    {
        _lerpStartPositionPlayer += Time.deltaTime * _speedRotateCamera;
        _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, _enterTransform.position, _lerpStartPositionPlayer);
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
        if (_lerpStartPositionPlayer > 1)
        {
            PlayerManager.Instance.IsInNarrowWay = true;
            _playerController.ChangeState(PlayerAgentController.MyState.NARROWWAY);
            GameLoopManager.Instance.LoopQTE -= OnStartFalling;
            _lerpStartPositionPlayer = 0;
        }
    }
}
