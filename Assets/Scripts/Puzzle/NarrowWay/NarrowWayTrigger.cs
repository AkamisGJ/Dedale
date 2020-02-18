using UnityEngine;

public class NarrowWayTrigger : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private float _lerpStartPositionPlayer = 0;
    private float _lerpExitNarrowWay = 0;
    private float _speedRotateCamera = 5;
    [SerializeField] private Collider _collider = null;
    [SerializeField] private Collider _pairedColliderNarowWay = null;
    [SerializeField] private Transform _enterTransform = null;
    [SerializeField] private Transform _exitTransform = null;

    void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        PlayerManager.Instance.IsInNarrowWay = false;
    }

    public void StartPositionPlayer()
    {
        if(_collider.isTrigger == false)
        {
            _collider.isTrigger = true;
            _pairedColliderNarowWay.isTrigger = true;
        }
        if(PlayerManager.Instance.IsInNarrowWay == false)
        {
            _lerpStartPositionPlayer += Time.deltaTime * _speedRotateCamera;
            _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, _enterTransform.position, _lerpStartPositionPlayer);
            _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
            _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
            if (_lerpStartPositionPlayer > 1)
            {
                PlayerManager.Instance.IsInNarrowWay = true;
                _playerController.ChangeState(PlayerAgentController.MyState.NARROWWAY);
                GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
                _lerpStartPositionPlayer = 0;
            }
        }
    }

    private void OnExitNarrowWayTrigger()
    {
        _lerpExitNarrowWay += Time.deltaTime * _speedRotateCamera;
        _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, _exitTransform.position, _lerpExitNarrowWay);
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _exitTransform.rotation, _lerpExitNarrowWay);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, _exitTransform.rotation, _lerpExitNarrowWay);
        if (_lerpExitNarrowWay >= 1)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
            PlayerManager.Instance.IsInNarrowWay = false;
            _lerpExitNarrowWay = 0;
            GameLoopManager.Instance.GameLoopPlayer -= OnExitNarrowWayTrigger;
            _collider.isTrigger = false;
            _pairedColliderNarowWay.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerManager.Instance.IsInNarrowWay == true)
        {
            GameLoopManager.Instance.GameLoopPlayer += OnExitNarrowWayTrigger;
        }
    }
}
