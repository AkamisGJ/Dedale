using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHeroManager : MonoBehaviour
{
    private float _moodMeter = 50;
    [SerializeField] private NoteDataBase[] _noteArray;
    private List<NoteDataBase> _noteList;
    private List<NoteDataBase> _nextNoteToAppear;
    [SerializeField] private Transform _spawnerLineA = null;
    [SerializeField] private Transform _spawnerLineZ = null;
    [SerializeField] private Transform _spawnerLineE = null;
    [SerializeField] private Transform _spawnerLineR = null;
    [SerializeField] private GameObject _notePrefabs = null;
    private bool _canSearchNextNote = true;
    private float _currentTime = 0;

    private void Start()
    {
        _noteList = new List<NoteDataBase>();
        _nextNoteToAppear = new List<NoteDataBase>();
        if(_noteArray.Length == 0)
        {
            this.enabled = false;
            return;
        }
        for (int i = 0; i < _noteArray.Length; i++)
        {
            _noteList.Add(_noteArray[i]);
        }
        _canSearchNextNote = true;
        _currentTime = 0;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_canSearchNextNote == true && _noteList.Count > 0)
        {
            SearchNextNode();
        }
        if(_canSearchNextNote == false && _nextNoteToAppear.Count > 0 && _nextNoteToAppear[0].TimeToAppear > _currentTime)
        {
            for (int i = 0; i < _nextNoteToAppear.Count; i++)
            {
                if (_nextNoteToAppear[i].StartLine == 0)
                {
                    Instantiate(_notePrefabs, _spawnerLineA.position, Quaternion.identity);
                }
                if (_nextNoteToAppear[i].StartLine == 1)
                {
                    Instantiate(_notePrefabs, _spawnerLineZ.position, Quaternion.identity);
                }
                if (_nextNoteToAppear[i].StartLine == 2)
                {
                    Instantiate(_notePrefabs, _spawnerLineE.position, Quaternion.identity);
                }
                if (_nextNoteToAppear[i].StartLine == 3)
                {
                    Instantiate(_notePrefabs, _spawnerLineR.position, Quaternion.identity);
                }
            }
            _nextNoteToAppear.Clear();
            _canSearchNextNote = true;
        }
    }

    private void SearchNextNode()
    {
        for (int i = 0; i < _noteList.Count; i++)
        {
            if (_nextNoteToAppear.Count != 0)
            {
                if (_nextNoteToAppear[0].TimeToAppear == _noteList[i].TimeToAppear)
                {
                    _nextNoteToAppear.Add(_noteList[i]);
                    _noteList.Remove(_noteList[i]);
                }
                if (_nextNoteToAppear[0].TimeToAppear > _noteList[i].TimeToAppear)
                {
                    for (int n = 0; n < _nextNoteToAppear.Count; n++)
                    {
                        _noteList.Add(_nextNoteToAppear[n]);
                    }
                    _nextNoteToAppear.Clear();
                    _nextNoteToAppear.Add(_noteList[i]);
                    _noteList.Remove(_noteList[i]);
                }
            }
            if (_nextNoteToAppear.Count == 0)
            {
                _nextNoteToAppear.Add(_noteList[0]);
            }
        }
        _canSearchNextNote = false;
    }
}
