using UnityEngine;

public class StartLadder : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private bool _canLeave = false;
    private float _lerpStartPositionPlayer = 0;
    [SerializeField] private QTEManager[] _qTEManagers = null;
    private Vector3 _startPlayerPosition = Vector3.zero;
    private Quaternion _startPlayerQuaternion = Quaternion.identity;
    private Quaternion _startCameraQuaternion = Quaternion.identity;
    private float _startFieldOfView = 0;
    private bool _isStarted = false;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _lerpStartPositionPlayer = 0;
        _isStarted = false;
        GameLoopManager.Instance.LadderLoop += OnUpdate;
    }

    private void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canLeave == true)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT, null,"Idle");
        }
    }

    public void StartPositionPlayer()
    {
        if (!_isStarted)
        {
            _startPlayerPosition = _playerController.transform.position;
            _startPlayerQuaternion = _playerController.transform.rotation;
            _startCameraQuaternion = _playerController.MainCamera.transform.rotation;
            _startFieldOfView = _playerController.MainCamera.fieldOfView;
            _playerController.AnimatorCamera.SetBool("Walk", true);
            _playerController.AnimatorCamera.SetBool("Run", false);
            _playerController.AnimatorCamera.SetBool("NoAnim", false);
            _playerController.AnimatorCamera.SetBool("Idle", false);
            _isStarted = true;
        }
        _lerpStartPositionPlayer += Time.deltaTime;
        _playerController.transform.position = Vector3.Lerp(_startPlayerPosition, transform.position, _lerpStartPositionPlayer);
        _playerController.transform.rotation = Quaternion.Lerp(_startPlayerQuaternion, transform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.transform.rotation = Quaternion.Lerp(_startCameraQuaternion, transform.rotation, _lerpStartPositionPlayer);
        _playerController.MainCamera.fieldOfView = Mathf.Lerp(_startFieldOfView, _playerController.PlayerData.FieldOfView, _lerpStartPositionPlayer);
        if(_lerpStartPositionPlayer > 1)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.QTELADDER, null, "Idle");
            GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
            _lerpStartPositionPlayer = 0;
            _isStarted = false;
            if(_qTEManagers.Length > 0)
            {
                Debug.Log(_qTEManagers.Length);
                foreach (QTEManager qTEManager in _qTEManagers)
                {
                    qTEManager.IsActive = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.LoopQTE -= StartPositionPlayer;
            GameLoopManager.Instance.LadderLoop -= OnUpdate;
        }
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
