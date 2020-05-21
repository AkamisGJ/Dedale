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

    void Awake()
    {
        _dialogueIndex = 0;
    }

    public void OnStart()
    {
        _dialogueManager.OnStartTimeline(_playableDirector, _dialogueData);
        if(_eventEmitter != null && _eventEmitter.IsPlaying() == false)
        {
            _eventEmitter.Play();
        }
        GameLoopManager.Instance.GameLoopLoadingScene += OnUpdate;
    }

    public void ClearText()
    {
        _dialogueManager.DestroyCurrentDialogue();
    }

    public void NextText()
    {
        if(_eventEmitter != null)
        {
            _eventEmitter.EventDescription.is3D(out _is3D);
        }
        if (_eventEmitter != null || _is3D == false || Vector3.Distance(PlayerManager.Instance.PlayerController.transform.position, _eventEmitter.transform.position) < _eventEmitter.OverrideMaxDistance)
        {
            if (_playableDirector == _dialogueManager.CurrentPlayableDirector)
            {
                _dialogueManager.OnChangeText(_dialogueData.Dialogue[_dialogueIndex]);
                _dialogueIndex += 1;
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

    private void OnUpdate()
    {
        if(GameLoopManager.Instance.IsPaused == true && _playableDirector.state == PlayState.Playing)
        {
            _playableDirector.Pause();
            if(_eventEmitter != null)
            {
                _eventEmitter.EventInstance.setPaused(true);
            }
        }
        else if(GameLoopManager.Instance.IsPaused == false && _playableDirector.state == PlayState.Paused)
        {
            _playableDirector.Resume();
            if (_eventEmitter != null)
            {
                _eventEmitter.EventInstance.setPaused(false);
            }
        }
    }

    public void OnExit()
    {
        _dialogueManager.OnEndTimeline();
        if(_eventEmitter != null)
        {
            _eventEmitter.Stop();
        }
        GameLoopManager.Instance.GameLoopLoadingScene -= OnUpdate;
        _dialogueIndex = 0;
    }
}