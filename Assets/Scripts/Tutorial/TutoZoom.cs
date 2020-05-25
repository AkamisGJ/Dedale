using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutoZoom : MonoBehaviour
{
    private float _fade = 0;
    private bool _fading = false;
    [SerializeField] private GameObject _tutoZoom = null;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    private Color _color = Color.white;
    [SerializeField] private RawImage _image = null;
    [SerializeField] private float _transpaencyMin = 0.8f;
    private bool _canFade = false;

    void Start()
    {
        _fading = false;
        _canFade = false;
        _fade = 0;
        _color = _image.color;
        _color.a = _fade;
        _image.color = _color;
        _textMeshPro.alpha = _fade;
    }

    public void OnStart()
    {
        InputManager.Instance.Zoom += VerifyTouch;
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
                    Destroy(_tutoZoom);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void VerifyTouch(bool isZooming)
    {
        if (isZooming == true)
        {
            _fading = true;
            InputManager.Instance.Zoom -= VerifyTouch;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager.Instance.Zoom += VerifyTouch;
            GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
        }
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopInputManager -= OnUpdate;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Zoom -= VerifyTouch;
        }
    }
}
