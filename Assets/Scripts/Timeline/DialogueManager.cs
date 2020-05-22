using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    private PlayableDirector _currentPlayableDirector = null;
    private PlayableDirector _nextPlayableDirector = null;
    private DialogueData _nextDialogueData = null;
    [SerializeField] private GameObject _canvasDialogue = null;
    [SerializeField] private GameObject _prefabsDialogue = null;
    [SerializeField] private float _positionYDialogue = -170;
    [SerializeField] private float _timeExposeDialogue = 1;
    private GameObject _previousDialogue = null;
    private List<GameObject> _dialogues = null;
    private string _nextText = null;
    private float _dialogueTransition = 0;
    private GameObject[] _dialogueToMove = null;

    public PlayableDirector CurrentPlayableDirector { get => _currentPlayableDirector; }
    public List<GameObject> Dialogues { get => _dialogues; set => _dialogues = value; }
    public float TimeExposeDialogue { get => _timeExposeDialogue; }

    void Awake()
    {
        _dialogues = new List<GameObject>();
        _dialogueTransition = 0;
    }

    public void OnStartTimeline(PlayableDirector playableDirector, DialogueData dialogueData)
    {
        if (_currentPlayableDirector == null)
        {
            _dialogueTransition = 0;
            _currentPlayableDirector = playableDirector;
            _currentPlayableDirector.Play();
        }
        else if(_currentPlayableDirector != playableDirector)
        {
            _nextPlayableDirector = playableDirector;
            _nextDialogueData = dialogueData;
        }
    }

    private void SpawnText()
    {
        if(_dialogues.Count > 0)
        {
            _dialogueToMove = new List<GameObject>(_dialogues).ToArray();
            GameLoopManager.Instance.LastStart += TransitionDIalogue;
        } 
        GameObject currentDialogue = Instantiate(_prefabsDialogue, _canvasDialogue.transform.position, Quaternion.identity, _canvasDialogue.transform);
        currentDialogue.transform.localPosition = new Vector3(0, _positionYDialogue, 0);
        TextMeshProUGUI textMeshProUGUI = currentDialogue.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = _nextText;
        _dialogues.Add(currentDialogue);
        if(_previousDialogue != null)
        {
            _previousDialogue.GetComponent<DialogueComportment>().SwitchToFadeOut();
        }
        _previousDialogue = currentDialogue;
        if (_nextPlayableDirector != null)
        {
            _currentPlayableDirector.GetComponent<DialogueScript>().OnExit();
            _currentPlayableDirector.Stop();
            _currentPlayableDirector = null;
            OnStartTimeline(_nextPlayableDirector, _nextDialogueData);
            _nextPlayableDirector = null;
        }
        GameLoopManager.Instance.LastStart -= SpawnText;
    }

    private void TransitionDIalogue()
    {
        _dialogueTransition += Time.deltaTime * 200;
        _dialogueTransition = Mathf.Clamp(_dialogueTransition, 0, 50);
        foreach (var dialogue in _dialogueToMove)
        {
            dialogue.transform.localPosition = new Vector3(0, _dialogueTransition, 0);
        }
        if (_dialogueTransition == 50)
        {
            _dialogueTransition = 0;
            GameLoopManager.Instance.LastStart -= TransitionDIalogue;
        }
    }

    public void OnChangeText(string text)
    {
        _nextText = text;
        GameLoopManager.Instance.LastStart += SpawnText;
    }

    public void DestroyCurrentDialogue()
    {
        if(_previousDialogue != null)
        {
            _previousDialogue.GetComponent<DialogueComportment>().SwitchToFadeOut();
        }
    }

    public void OnEndTimeline()
    {
        if(_currentPlayableDirector != null)
        {
            _currentPlayableDirector.Stop();
        }
        _currentPlayableDirector = null;
        DestroyCurrentDialogue();
        if (_nextPlayableDirector != null)
        {
            OnStartTimeline(_nextPlayableDirector, _nextDialogueData);
            _nextPlayableDirector = null;
        }
    }
}