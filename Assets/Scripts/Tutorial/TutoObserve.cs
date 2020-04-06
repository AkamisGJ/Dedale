using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutoObserve : MonoBehaviour
{
    private float _fade = 0;
    private bool _fading = false;
    [SerializeField] private GameObject _tutoObserve = null;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    private Color _color = Color.white;
    [SerializeField] private RawImage _image = null;
    [SerializeField] private float _transpaencyMin = 0.8f;
    private bool _canFade = false;
    private bool _interactObject = false;

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
        GameLoopManager.Instance.GameLoopInputManager += VerifyTouch;
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
                    Destroy(_tutoObserve);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void VerifyTouch()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameLoopManager.Instance.GameLoopInputManager += VerifyStates;
            GameLoopManager.Instance.GameLoopInputManager -= VerifyTouch;
        }
    }

    private void VerifyStates()
    {
        if(PlayerManager.Instance.PlayerController.CurrentState == PlayerAgentController.MyState.OBSERVE)
        {
            _fading = true;
        }
        else
        {
            GameLoopManager.Instance.GameLoopInputManager += VerifyTouch;
        }
        GameLoopManager.Instance.GameLoopInputManager -= VerifyStates;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameLoopManager.Instance.GameLoopInputManager += VerifyTouch;
            GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
        }
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GameLoopInputManager -= OnUpdate;
        GameLoopManager.Instance.GameLoopInputManager -= VerifyTouch;
    }
}
