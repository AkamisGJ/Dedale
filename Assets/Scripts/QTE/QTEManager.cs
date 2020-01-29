using System.Collections;
using System.Collections.Generic;
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

    public void Start()
    {
        _onWaitingInput = false;
        _timeQTE = 0;
        _currentQTEIndex = 0;
        _currentGauge = 0;
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        _maxGauge = _currentQTE.MaxGauge;
        _isWin = false;
        InputManager.Instance.QTE += VerifTouch;
    }

    void Update()
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
    }

    void ValidQTE()
    {
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        _maxGauge = _currentQTE.MaxGauge;
        Debug.Log(_currentQTE.KeyCode + "    Valid");
    }


    void FailQTE()
    {
        Debug.Log("FAil");
        _currentQTEIndex = 0;
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        Debug.Log(_currentQTE.KeyCode +"   Fail");
        _timeQTE = 0;
    }

    private void OnDestroy()
    {
        InputManager.Instance.QTE -= VerifTouch;
    }
}
