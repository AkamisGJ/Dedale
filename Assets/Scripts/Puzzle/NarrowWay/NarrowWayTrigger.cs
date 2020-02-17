using UnityEngine;

public class NarrowWayTrigger : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private float _lerpStartPositionPlayer = 0;
    private bool _isInNarrowWay = false;

    void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _isInNarrowWay = false;
    }

    public void StartPositionPlayer()
    {
        if(_isInNarrowWay == false)
        {
            _lerpStartPositionPlayer += Time.deltaTime;
            _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, transform.position, _lerpStartPositionPlayer);
            _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, transform.rotation, _lerpStartPositionPlayer);
            _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, transform.rotation, _lerpStartPositionPlayer);
            if (_lerpStartPositionPlayer > 1)
            {
                _isInNarrowWay = true;
                _playerController.ChangeState(PlayerAgentController.MyState.NARROWWAY);
                GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
                _lerpStartPositionPlayer = 0;
            }
        }
    }
}
