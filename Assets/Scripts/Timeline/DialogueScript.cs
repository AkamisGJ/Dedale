using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private DialogueData _dialogueData = null;
    [SerializeField] private TextMeshProUGUI _textMeshPro = null;
    [SerializeField] private GameObject _canvasDialogue = null;
    [SerializeField] private DialogueManager _dialogueManager = null;
    private int _dialogueIndex = 0;

    public DialogueData DialogueData { get => _dialogueData; }

    void Start()
    {
        _dialogueIndex = 0;
    }

    public void OnStart()
    {
        _dialogueManager.OnStartTimeline(_playableDirector, _dialogueData);
    }

    public void NextText()
    {
        if(_playableDirector == _dialogueManager.CurrentPlayableDirector)
        {
            _dialogueIndex += 1;
            _dialogueManager.OnChangeText(_dialogueData.Dialogue[_dialogueIndex]);
        }
        else
        {
            _dialogueIndex = 0;
        }
    }

    public void OnExit()
    {
        _dialogueManager.OnEndTimeline();
        _dialogueIndex = 0;
    }
}