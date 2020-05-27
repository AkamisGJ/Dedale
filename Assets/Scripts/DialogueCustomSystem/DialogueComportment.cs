using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueComportment : MonoBehaviour
{
    private DialogueManager _dialogueManager = null;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    [SerializeField] private RawImage _image = null;
    [SerializeField] private float _transpaencyMin = 0.8f; 
    private bool _fading = false;
    private bool _canFade = false;
    private float _fade = 0;
    private Color _color = Color.white;
    private float _timeAppear = 0;

    void Start()
    {
        _dialogueManager = GetComponentInParent<DialogueManager>();
        _fading = false;
        _canFade = false;
        _fade = 0;
        _color = _image.color;
        _color.a = _fade;
        _image.color = _color;
        _textMeshPro.alpha = _fade;
        GameLoopManager.Instance.GameLoopPlayer += OnUpdate;
    }

    void OnUpdate()
    {
        if(_fading == false)
        {
            _fade = Mathf.Clamp(_fade + Time.deltaTime * 2, 0, 1);
            _textMeshPro.alpha = _fade;
            _color.a = _fade * _transpaencyMin;
            _image.color = _color;
        }
        else if(_fading == true)
        {
            _timeAppear += Time.deltaTime;
            if(_timeAppear >= _dialogueManager.TimeExposeDialogue)
            {
                _canFade = true;
            }
            if (_canFade == true)
            {
                _fade = Mathf.Clamp(_fade - Time.deltaTime * 2, 0, 1);
                _textMeshPro.alpha = _fade;
                _color.a = _fade * _transpaencyMin;
                _image.color = _color;
                if (_fade == 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SwitchToFadeOut()
    {
        _fading = true;
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopPlayer -= OnUpdate;
        }
        _dialogueManager.Dialogues.Remove(this.gameObject);
    }
}