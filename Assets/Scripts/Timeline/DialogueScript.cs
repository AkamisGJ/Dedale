using UnityEngine;
using UnityEngine.Playables;
using FMODUnity;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector = null;
    [SerializeField] private DialogueData _dialogueData = null;
    [SerializeField] private DialogueManager _dialogueManager = null;
    [SerializeField] private StudioEventEmitter _eventEmitter = null;
    private int _dialogueIndex = 0;
    private bool _is3D = false;

    public DialogueData DialogueData { get => _dialogueData; }

    void Start()
    {
        _dialogueIndex = 0;
    }

    public void OnStart()
    {
        _dialogueManager.OnStartTimeline(_playableDirector, _dialogueData);
        if(_eventEmitter.IsPlaying() == false)
        {
            _eventEmitter.Play();
        }
    }

    public void NextText()
    {
        _eventEmitter.EventDescription.is3D(out _is3D);
        if (Vector3.Distance(PlayerManager.Instance.PlayerController.transform.position, _eventEmitter.transform.position) < _eventEmitter.OverrideMaxDistance || _is3D == false)
        {
            if (_playableDirector == _dialogueManager.CurrentPlayableDirector)
            {
                _dialogueIndex += 1;
                _dialogueManager.OnChangeText(_dialogueData.Dialogue[_dialogueIndex]);
            }
            else
            {
                _dialogueIndex = 0;
            }
        }
        else
        {
            _dialogueIndex += 1;
            _dialogueManager.DestroyCurrentDialogue();
        }
    }

    public void OnExit()
    {
        _dialogueManager.OnEndTimeline();
        _eventEmitter.Stop();
        _dialogueIndex = 0;
    }
}