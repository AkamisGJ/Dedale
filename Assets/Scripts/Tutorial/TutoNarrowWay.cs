using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutoNarrowWay : MonoBehaviour
{
    private float _fade = 0;
    private bool _fading = false;
    [SerializeField] private GameObject _tutoMovement = null;
    [SerializeField] private TextMeshProUGUI _textFirstPart = null;
    [SerializeField] private TextMeshProUGUI _textSecondPart = null;
    private Color _color = Color.white;
    [SerializeField] private RawImage _imageFirstPart = null;
    [SerializeField] private RawImage _imageSecondPart = null;
    [SerializeField] private float _transpaencyMin = 0.8f;
    private bool _canFade = false;
    private bool _firstPartIsDone = false;
    private float _currentTime = 0;
    private bool _tutoGoBack = false;

    void Start()
    {
        _fading = false;
        _canFade = false;
        _fade = 0;
        _color = _imageFirstPart.color;
        _color.a = _fade;
        _imageFirstPart.color = _color;
        _color = _imageSecondPart.color;
        _color.a = _fade;
        _imageSecondPart.color = _color;
        _textFirstPart.alpha = _fade;
        _textSecondPart.alpha = _fade;
        _firstPartIsDone = false;
        _tutoGoBack = false;
    }

    public void OnStart()
    {
        GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
    }

    public void OnEnterNarrowWay()
    {
        _fading = true;
    }

    public void OnExitNarrowWay()
    {
        _fading = true;
    }

    public void OnExitNarrowWayWrongWay()
    {
        _fading = true;
        _tutoGoBack = true;
    }

    void OnUpdate()
    {
        if (_fade == 1)
        {
            _canFade = true;
        }
        if (_firstPartIsDone == false && (_fading == false || _canFade == false))
        {
            _fade = Mathf.Clamp(_fade + Time.deltaTime * 2, 0, 1);
            _textFirstPart.alpha = _fade;
            _color.a = _fade * _transpaencyMin;
            _imageFirstPart.color = _color;
        }
        else if (_firstPartIsDone == false && _fading == true)
        {
            if (_canFade == true)
            {
                _fade = Mathf.Clamp(_fade - Time.deltaTime * 2, 0, 1);
                _textFirstPart.alpha = _fade;
                _color.a = _fade * _transpaencyMin;
                _imageFirstPart.color = _color;
                if (_fade == 0)
                {
                    _firstPartIsDone = true;
                    _fade = 0;
                    _fading = false;
                    _canFade = false;
                }
            }
        }
        else if (_firstPartIsDone == true && (_fading == false || _canFade == false ))
        {
            _fade = Mathf.Clamp(_fade + Time.deltaTime * 2, 0, 1);
            _textSecondPart.alpha = _fade;
            _color.a = _fade * _transpaencyMin;
            _imageSecondPart.color = _color;
        }
        else if (_firstPartIsDone == true && _fading == true)
        {
            if (_canFade == true)
            {
                _fade = Mathf.Clamp(_fade - Time.deltaTime * 2, 0, 1);
                _textSecondPart.alpha = _fade;
                _color.a = _fade * _transpaencyMin;
                _imageSecondPart.color = _color;
                if (_fade == 0 && _tutoGoBack == false)
                {
                    Destroy(_tutoMovement);
                    Destroy(this.gameObject);
                }
                else if(_fade == 0 && _tutoGoBack == true)
                {
                    _firstPartIsDone = false;
                    _fade = 0;
                    _fading = false;
                    _canFade = false;
                    _tutoGoBack = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
        }
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopInputManager -= OnUpdate;
        }
    }
}
