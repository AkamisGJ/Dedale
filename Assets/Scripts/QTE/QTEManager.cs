using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private QTEDataBase[] _qTEDataBases = null;
    private float _timeQTE = 0;
    private int _currentQTEIndex = 0;
    private QTEDataBase _currentQTE = null;
    private float _maxGauge = 100;
    private float _currentGauge = 0;
    private bool _isWin = false;
    private PlayerAgentController _playerController = null;
    private IQTELadder _iQTELadder = null;
    [SerializeField] private TextMeshPro _touchVisual = null;
    [SerializeField] private Image _spamImage = null;
    [SerializeField] private Image _uniqueImage = null;
    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _touchVisual.gameObject.SetActive(false);
        _spamImage.gameObject.SetActive(false);
        _uniqueImage.gameObject.SetActive(false);
        _isActive = true;
    }

    private void OnStart()
    {
        if(_isActive == true)
        {
            if (_playerController.CurrentState == PlayerAgentController.MyState.QTELADDER)
            {
                _iQTELadder = _playerController.States[PlayerAgentController.MyState.QTELADDER] as IQTELadder;
                _iQTELadder.CanMove = false;
            }
            _timeQTE = 0;
            _currentQTEIndex = 0;
            _currentGauge = _maxGauge / 2;
            _currentQTE = _qTEDataBases[_currentQTEIndex];
            _touchVisual.text = _qTEDataBases[_currentQTEIndex].KeyCode.ToString();
            _maxGauge = _currentQTE.MaxGauge;
            _isWin = false;
            InputManager.Instance.QTE += VerifTouch;
            GameLoopManager.Instance.LoopQTE += OnUpdate;
        }
    }

    void OnUpdate()
    {
        if (_isWin == false)
        {
            _timeQTE += Time.deltaTime;
            if (_timeQTE > _currentQTE.TimeToAppear && _timeQTE < _currentQTE.TimeToAppear + _currentQTE.TimeToMake)
            {
                _touchVisual.gameObject.SetActive(true);
                if (_currentQTE.SpamQTE == true)
                {
                    _spamImage.gameObject.SetActive(true);
                    _spamImage.fillAmount = _currentGauge / _maxGauge;
                    _currentGauge -= _currentQTE.LosePerSecond * Time.deltaTime;
                    if (_currentGauge <= 0)
                    {
                        FailQTE();
                    }
                }
                else
                {
                    _uniqueImage.gameObject.SetActive(true);
                    _uniqueImage.rectTransform.sizeDelta = new Vector2(1, 1) * (_currentQTE.TimeToMake + _currentQTE.TimeToAppear - _timeQTE)/ (_currentQTE.TimeToMake + _currentQTE.TimeToAppear);
                }
                Debug.Log(_currentQTE.KeyCode + "   appear");
            }
            if (_timeQTE > _currentQTE.TimeToAppear + _currentQTE.TimeToMake)
            {
                _spamImage.gameObject.SetActive(false);
                _touchVisual.gameObject.SetActive(false);
                FailQTE();
            }
        }
        else
        {
            InputManager.Instance.QTE -= VerifTouch;
            GameLoopManager.Instance.LoopQTE -= OnUpdate;
            _iQTELadder.CanMove = true;
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
                            _spamImage.gameObject.SetActive(false);
                            _touchVisual.gameObject.SetActive(false);
                            ValidQTE();
                        }
                        else
                        {
                            _spamImage.gameObject.SetActive(false);
                            _touchVisual.gameObject.SetActive(false);
                            _isWin = true;
                            _isActive = false;
                            Debug.Log("ok");
                        }
                    }
                }
                else
                {
                    _uniqueImage.gameObject.SetActive(false);
                    Debug.Log("ok");
                    _currentQTEIndex += 1;
                    if(_qTEDataBases.Length > _currentQTEIndex)
                    {
                        _spamImage.gameObject.SetActive(false);
                        _touchVisual.gameObject.SetActive(false);
                        ValidQTE();
                    }
                    else
                    {
                        _spamImage.gameObject.SetActive(false);
                        _touchVisual.gameObject.SetActive(false);
                        _isWin = true;
                        _isActive = false;
                    }
                }
            }
        }
        if(_timeQTE > _currentQTE.TimeToMake + _currentQTE.TimeToAppear && _isWin == false)
        {
            FailQTE();
        }
    }

    void ValidQTE()
    {
        _touchVisual.text = _qTEDataBases[_currentQTEIndex].KeyCode.ToString();
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        _maxGauge = _currentQTE.MaxGauge;
        _currentGauge = _maxGauge / 2;
        _timeQTE = 0;
        _uniqueImage.gameObject.SetActive(false);
        _spamImage.gameObject.SetActive(false);
        _touchVisual.gameObject.SetActive(false);
        Debug.Log(_currentQTE.KeyCode + "    Valid");
    }

    void FailQTE()
    {
        Debug.Log("FAil");
        _timeQTE = 0;
        _currentGauge = _maxGauge / 2;
        _uniqueImage.gameObject.SetActive(false);
        _spamImage.gameObject.SetActive(false);
        _touchVisual.gameObject.SetActive(false);
        _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        GameLoopManager.Instance.LoopQTE -= OnUpdate;
        _iQTELadder.CanMove = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
