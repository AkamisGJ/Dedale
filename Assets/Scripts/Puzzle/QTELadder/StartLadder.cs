using UnityEngine;

public class StartLadder : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private IMouvement _mouvement = null;
    private bool _canLeave = false;
    private float _lerpStartPositionPlayer = 0;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _lerpStartPositionPlayer = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canLeave == true)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }

    public void StartPositionPlayer()
    {
        _lerpStartPositionPlayer += Time.deltaTime;
        _playerController.transform.position = Vector3.Lerp(_playerController.transform.position, transform.position, _lerpStartPositionPlayer);
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, transform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_playerController.MainCamera.transform.rotation, transform.rotation, _lerpStartPositionPlayer);
        if(_lerpStartPositionPlayer > 1)
        {
            GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
        }
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canLeave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canLeave = false;
        }
    }
}
