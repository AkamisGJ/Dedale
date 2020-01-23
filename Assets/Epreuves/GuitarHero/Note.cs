using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _minTimeToSwitch = 0.5f;
    [SerializeField] private float _maxTimeToSwitch = 2;
    [SerializeField] private float _speedLerp = 1;
    private NoteDataBase _dataBase = null;
    private Transform _lineToSwitch;
    private float _currentTime = 0;
    private bool _onStartLine = true;
    private float _trustMeter = 0;
    private float _timeToSwitch = 1;
    private Transform _startLine = null;
    private Vector3 targetPosition = Vector3.zero;
    private bool _inTranslation = false;
    private float _lerpFloat = 0;
    private Vector3 _originPosition = Vector3.zero;

    public NoteDataBase DataBase { get => _dataBase; set => _dataBase = value; }
    public Transform LineToSwitch { get => _lineToSwitch; set => _lineToSwitch = value; }
    public float TrustMeter { get => _trustMeter; set => _trustMeter = value; }
    public Transform StartLine { get => _startLine; set => _startLine = value; }

    private void Start()
    {
        Debug.Log(Time.time);
        _rb2D.velocity = -_speed * transform.up;
        _currentTime = 0;
        _onStartLine = true;
        _timeToSwitch = Random.Range(_minTimeToSwitch, _maxTimeToSwitch);
        _inTranslation = false;
        _lerpFloat = 0;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > _timeToSwitch && transform.position.y > 0.2)
        {
            if(_trustMeter < Random.Range(0, 100))
            {
                _trustMeter += 10;
                if (_onStartLine)
                {
                    _originPosition = transform.position;
                    targetPosition = new Vector3(_lineToSwitch.position.x, transform.position.y - _speed, transform.position.z);
                    _rb2D.velocity = Vector2.zero;
                    _onStartLine = false;
                    _timeToSwitch += _timeToSwitch;
                    _lerpFloat = 0;
                    _inTranslation = true;
                }
                else
                {
                    _originPosition = transform.position;
                    targetPosition = new Vector3(_startLine.position.x, transform.position.y - _speed, transform.position.z);
                    _rb2D.velocity = Vector2.zero;
                    _onStartLine = true;
                    _timeToSwitch += _timeToSwitch;
                    _lerpFloat = 0;
                    _inTranslation = true;
                }
            }
            else
            {
                if (_trustMeter != 100)
                {
                    _trustMeter -= 10;
                    _timeToSwitch += _timeToSwitch;
                }
            }
        }
        if (_inTranslation)
        {
            _lerpFloat += Time.deltaTime * _speedLerp;
            _lerpFloat = Mathf.Clamp(_lerpFloat, 0, 1);
            if(_lerpFloat == 1)
            {
                _rb2D.velocity = -_speed * transform.up;
                _inTranslation = false;
                return;
            }
            transform.position = Vector3.Lerp(_originPosition, targetPosition, _lerpFloat);
        }
    }
}