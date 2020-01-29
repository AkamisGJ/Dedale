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

    public void Start()
    {
        _onWaitingInput = false;
        _timeQTE = 0;
        _currentQTEIndex = 0;
        _currentGauge = 0;
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        _maxGauge = _currentQTE.MaxGauge;
        InputManager.Instance.QTE += VerifTouch;
    }

    void Update()
    {
        _timeQTE += Time.deltaTime;
        if(_timeQTE > _currentQTE.TimeToAppear)
        {
            Debug.Log(_currentQTE.KeyCode);
        }
        if(_timeQTE > _currentQTE.TimeToAppear + _currentQTE.TimeToMake)
        {
            FailQTE();
        }
    }

    void VerifTouch(KeyCode keyCode)
    {
        if(_timeQTE > _currentQTE.TimeToAppear)
        {
            Debug.Log(_currentQTE.KeyCode);
            if (keyCode == _currentQTE.KeyCode)
            {
                if(_currentQTE.SpamQTE == true)
                {
                    if(_currentGauge < _maxGauge)
                    {
                        _currentGauge += _currentQTE.GainGaugePerTouch;
                        Debug.Log(_currentGauge);
                    }
                    if(_currentGauge >= _maxGauge)
                    {
                        _currentQTEIndex += 1;
                        if (_qTEDataBases.Length > _currentQTEIndex)
                        {
                            _currentQTE = _qTEDataBases[_currentQTEIndex];
                            _maxGauge = _currentQTE.MaxGauge;
                        }
                        else
                        {
                            Debug.Log("WIn");
                        }
                    }
                }
                else
                {
                    Debug.Log("ok");
                    _currentQTEIndex += 1;
                    if(_qTEDataBases.Length > _currentQTEIndex)
                    {
                        _currentQTE = _qTEDataBases[_currentQTEIndex];
                        _maxGauge = _currentQTE.MaxGauge;
                        Debug.Log(_currentQTE.KeyCode);
                    }
                    else
                    {
                        Debug.Log("WIn");
                    }
                }
            }
        }
    }

    void FailQTE()
    {
        Debug.Log("FAil");
        _currentQTEIndex = 0;
        _currentQTE = _qTEDataBases[_currentQTEIndex];
        Debug.Log(_currentQTE.KeyCode);
        _timeQTE = 0;
    }

    private void OnDestroy()
    {
        InputManager.Instance.QTE -= VerifTouch;
    }
}
