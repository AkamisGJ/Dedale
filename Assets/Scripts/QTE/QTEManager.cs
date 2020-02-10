using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private QTEDataBase[] _qTEDataBases = null;
    private bool _onWaitingInput = false;
    private float _timeQTE = 0;
    private int _currentQTEIndex = 0;
    private QTEDataBase _currentQTE = null;
    private float _maxGauge = 100;
    private float _currentGauge = 0;
    private bool _isWin = false;
    private PlayerAgentController _playerController = null;
    private IQTELadder _iQTELadder = null;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    private void OnStart()
    {
        if(_playerController.CurrentState == PlayerAgentController.MyState.QTELADDER)
        {
            _iQTELadder = _playerController.GetComponent<IPlayerState>() as IQTELadder;
            _iQTELadder.CanMove = false;
            return;
        }
        _onWaitingInput = false;
        _timeQTE = 0;
        _currentQTEIndex = 0;
        _currentGauge = 0;
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        _maxGauge = _currentQTE.MaxGauge;
        _isWin = false;
        InputManager.Instance.QTE += VerifTouch;
        GameLoopManager.Instance.LoopQTE += OnUpdate;
    }

    void OnUpdate()
    {
        if(_isWin == false)
        {
            _timeQTE += Time.deltaTime;
            if(_timeQTE > _currentQTE.TimeToAppear && _timeQTE < _currentQTE.TimeToAppear + _currentQTE.TimeToMake)
            {
                Debug.Log(_currentQTE.KeyCode + "   appear");
            }
            if( _timeQTE > _currentQTE.TimeToAppear + _currentQTE.TimeToMake)
            {
                FailQTE();
            }
        }
    }

    void VerifTouch(KeyCode keyCode)
    {
        if(_timeQTE > _currentQTE.TimeToAppear && _isWin == false)
        {
            if (keyCode == _currentQTE.KeyCode)
            {
                if(_currentQTE.SpamQTE == true)
                {
                    _currentGauge -= _currentQTE.LosePerSecond * Time.deltaTime;
                    if(_currentGauge < _maxGauge)
                    {
                        _currentGauge += _currentQTE.GainGaugePerTouch;
                        Debug.Log(_currentGauge + "    /    " + _maxGauge);
                    }
                    if(_currentGauge >= _maxGauge)
                    {
                        _currentQTEIndex += 1;
                        if (_qTEDataBases.Length > _currentQTEIndex)
                        {
                            ValidQTE();
                        }
                        else
                        {
                            _isWin = true;
                            Debug.Log("Win");
                        }
                    }
                }
                else
                {
                    Debug.Log("ok");
                    _currentQTEIndex += 1;
                    if(_qTEDataBases.Length > _currentQTEIndex)
                    {
                        ValidQTE();
                    }
                    else
                    {
                        Debug.Log("Win");
                    }
                }
            }
        }
        if(_timeQTE > _currentQTE.TimeToMake && _isWin == false)
        {
            FailQTE();
        }
    }

    void ValidQTE()
    {
        _isWin = true;
        Debug.Log(_currentQTE.KeyCode + "    Valid");
    }

    void FailQTE()
    {
        Debug.Log("FAil");
        _timeQTE = 0;
        _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnStart();
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.QTE -= VerifTouch;
        GameLoopManager.Instance.LoopQTE -= OnUpdate;
    }
}
