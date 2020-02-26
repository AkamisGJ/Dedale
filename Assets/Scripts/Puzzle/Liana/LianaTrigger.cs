using UnityEngine;

public class LianaTrigger : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private float _lerpStartPositionPlayer = 0;
    private float _lerpExitNarrowWay = 0;
    private float _speedRotateCamera = 5;
    [SerializeField] private Collider _collider = null;
    [SerializeField] private Transform _enterTransform = null;

    void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    public void StartPositionPlayer()
    {
        _collider.isTrigger = true;
        _lerpStartPositionPlayer += Time.deltaTime * _speedRotateCamera;
        _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, _enterTransform.position, _lerpStartPositionPlayer);
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
        if (_lerpStartPositionPlayer > 1)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.LIANA);
            GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
        }
    }
}
