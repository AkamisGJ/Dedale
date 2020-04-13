using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutoMovement : MonoBehaviour
{
    private float _fade = 0;
    private bool _fading = false;
    [SerializeField] private GameObject _tutoMovement = null;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    private Color _color = Color.white;
    [SerializeField] private RawImage _image = null;
    [SerializeField] private float _transpaencyMin = 0.8f;
    private bool _canFade = false;
    private bool _z = false;
    private float _currentTime = 0;
    [SerializeField] private float _timeToWait = 3;

    void Start()
    {
        _fading = false;
        _canFade = false;
        _fade = 0;
        _color = _image.color;
        _color.a = _fade;
        _image.color = _color;
        _textMeshPro.alpha = _fade;
        _currentTime = 0;
    }

    public void OnStart()
    {
        InputManager.Instance.Direction += VerifyTouch;
        GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
    }

    void OnUpdate()
    {
        if (_fade == 1)
        {
            _canFade = true;
        }
        if (_fading == false || _canFade == false)
        {
            _fade = Mathf.Clamp(_fade + Time.deltaTime * 2, 0, 1);
            _textMeshPro.alpha = _fade;
            _color.a = _fade * _transpaencyMin;
            _image.color = _color;
        }
        else if (_fading == true)
        {
            if (_canFade == true)
            {
                _fade = Mathf.Clamp(_fade - Time.deltaTime * 2, 0, 1);
                _textMeshPro.alpha = _fade;
                _color.a = _fade * _transpaencyMin;
                _image.color = _color;
                if (_fade == 1)
                {
                    Destroy(_tutoMovement);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void VerifyTouch(float horizontalMouvement, float verticalMouvement)
    {
        if(horizontalMouvement > 0)
        {
            _z = true;
        }
        if(_z == true)
        {
            GameLoopManager.Instance.GameLoopInputManager += Timer;
            InputManager.Instance.Direction -= VerifyTouch;
        }
    }

    private void Timer()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _timeToWait)
        {
            _fading = true;
            GameLoopManager.Instance.GameLoopInputManager -= Timer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager.Instance.Direction += VerifyTouch;
            GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
        }
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GameLoopInputManager -= OnUpdate;
        InputManager.Instance.Direction -= VerifyTouch;
        GameLoopManager.Instance.GameLoopInputManager -= Timer;
    }
}
