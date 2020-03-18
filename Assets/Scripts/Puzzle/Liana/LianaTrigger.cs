using UnityEngine;

public class LianaTrigger : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private float _lerpStartPositionPlayer = 0;
    private float _lerpExitNarrowWay = 0;
    private float _speedRotateCamera = 5;
    [SerializeField] private Collider _collider = null;
    [SerializeField] private Transform _enterTransform = null;
    private Vector3 _startPlayerPosition = Vector3.zero;
    private Quaternion _startPlayerQuaternion = Quaternion.identity;
    private Quaternion _startCameraQuaternion = Quaternion.identity;
    private float _startFieldOfView = 0;
    private bool _isStarted = false;

    void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    public void StartPositionPlayer()
    {
        if (!_isStarted)
        {
            _startPlayerPosition = _playerController.transform.position;
            _startPlayerQuaternion = _playerController.transform.rotation;
            _startCameraQuaternion = _playerController.MainCamera.transform.rotation;
            _startFieldOfView = _playerController.MainCamera.fieldOfView;
            _isStarted = true;
        }
        _collider.isTrigger = true;
        _lerpStartPositionPlayer += Time.deltaTime * _speedRotateCamera;
        _playerController.transform.position = Vector3.Lerp(_startPlayerPosition, _enterTransform.position, _lerpStartPositionPlayer);
        _playerController.transform.rotation = Quaternion.Lerp(_startPlayerQuaternion, _enterTransform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_startCameraQuaternion, _enterTransform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.fieldOfView = Mathf.Lerp(_startFieldOfView, _playerController.PlayerData.FieldOfView, _lerpStartPositionPlayer);
        if (_lerpStartPositionPlayer > 1)
        {
            _isStarted = false;
            _playerController.ChangeState(PlayerAgentController.MyState.LIANA);
            GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
        }
    }
}
