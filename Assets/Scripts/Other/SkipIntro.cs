using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkipIntro : MonoBehaviour
{
    [SerializeField] UnityEvent _onSkipIntro = null;
    [SerializeField] Slider _sliderObject = null;
    private float _progressBar = 0;
    [SerializeField] float _speed = 2;
    private bool _IsPressed = false;

    void Start()
    {
        _IsPressed = false;
        _progressBar = 0;
        GameLoopManager.Instance.LoopQTE += OnUpdate;
    }

    private void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E) == true)
        {
            _IsPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.E) == true)
        {
            _IsPressed = false;
        }
        if(_IsPressed == true)
        {
            ProgressBar(1);
        }
        else
        {
            ProgressBar(-1);
        }
    }

    void ProgressBar(int inversion)
    {
        _progressBar += Time.deltaTime * _speed * inversion;
        _progressBar = Mathf.Clamp(_progressBar, 0, 1);
        _sliderObject.value = _progressBar;
        if (_progressBar == 1)
        {
            OnSkipIntro();
            GameLoopManager.Instance.LoopQTE -= OnUpdate;
        }
    }

    public void OnSkipIntro()
    {
        _onSkipIntro.Invoke();
    }
}
