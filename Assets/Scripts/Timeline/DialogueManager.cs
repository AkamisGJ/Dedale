﻿using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private PlayableDirector _currentPlayableDirector = null;
    private PlayableDirector _nextPlayableDirector = null;
    private DialogueData _nextDialogueData = null;
    [SerializeField] private GameObject _canvasDialogue = null;
    [SerializeField] private GameObject _prefabsDialogue = null;
    [SerializeField] private float _positionYDialogue = -170;
    [SerializeField] private float _timeExposeDialogue = 1;
    private List<GameObject> _dialogues = null;

    public PlayableDirector CurrentPlayableDirector { get => _currentPlayableDirector; }
    public List<GameObject> Dialogues { get => _dialogues; set => _dialogues = value; }

    void Start()
    {
        _dialogues = new List<GameObject>();
    }

    public void OnStartTimeline(PlayableDirector playableDirector, DialogueData dialogueData)
    {
        if(_currentPlayableDirector == null)
        {
            _currentPlayableDirector = playableDirector;
            _currentPlayableDirector.Play();
            GameObject _currentDialogue = Instantiate(_prefabsDialogue, _canvasDialogue.transform.position, Quaternion.identity, _canvasDialogue.transform);
            _currentDialogue.transform.localPosition = new Vector3(0, _positionYDialogue, 0);
            TextMeshProUGUI textMeshProUGUI = _currentDialogue.GetComponentInChildren<TextMeshProUGUI>();
            textMeshProUGUI.text = dialogueData.Dialogue[0];
            _dialogues.Add(_currentDialogue);
            Destroy(_currentDialogue, _timeExposeDialogue);
        }
        else if(_currentPlayableDirector != playableDirector)
        {
            _nextPlayableDirector = playableDirector;
            _nextDialogueData = dialogueData;
        }
    }

    public void OnChangeText(string text)
    {
        if(_dialogues.Count > 0)
        {
            foreach (var dialogue in _dialogues)
            {
                dialogue.transform.localPosition += new Vector3(0, 50, 0);
            }
        }
        GameObject _currentDialogue = Instantiate(_prefabsDialogue, _canvasDialogue.transform.position, Quaternion.identity, _canvasDialogue.transform);
        _currentDialogue.transform.localPosition = new Vector3(0, _positionYDialogue, 0);
        TextMeshProUGUI textMeshProUGUI = _currentDialogue.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = text;
        _dialogues.Add(_currentDialogue);
        Destroy(_currentDialogue, _timeExposeDialogue);
        if (_nextPlayableDirector != null)
        {
            _currentPlayableDirector.GetComponent<DialogueScript>().OnExit();
            _currentPlayableDirector.Stop();
            _currentPlayableDirector = null;
            OnStartTimeline(_nextPlayableDirector, _nextDialogueData);
            _nextPlayableDirector = null;
        }
    }

    public void OnEndTimeline()
    {
        _currentPlayableDirector = null;
        if (_nextPlayableDirector != null)
        {
            OnStartTimeline(_nextPlayableDirector, _nextDialogueData);
            _nextPlayableDirector = null;
        }
    }
}